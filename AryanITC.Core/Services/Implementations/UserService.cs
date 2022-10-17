using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AryanITC.Core.Convertor;
using AryanITC.Core.Extensions;
using AryanITC.Core.Generator;
using AryanITC.Core.Security;
using AryanITC.Core.Sender;
using AryanITC.Core.Senders;
using AryanITC.Core.Services.Interfaces;
using AryanITC.Domain.Entities.Access;
using AryanITC.Domain.Entities.Account;
using AryanITC.Domain.IRepository;
using AryanITC.Domain.ViewModels.Account;
using AryanITC.Domain.ViewModels.ManagementUser;
using Microsoft.AspNetCore.Routing.Template;
using static AryanITC.Domain.ViewModels.Account.LoginUserViewModel;



namespace AryanITC.Core.Services.Implementations
{
    public class UserService : IUserService
    {
        #region Constructor

        private readonly IUserRepository _userRepository;
        private readonly IViewRenderService _viewRender;

        public UserService(IUserRepository userRepository, IViewRenderService viewRender)
        {
            _userRepository = userRepository;
            _viewRender = viewRender;
        }


        #endregion

        #region Account

        public async Task<RegisterUserResult> RegisterUser(RegisterUserViewModel registerUserViewModel)
        {

            var emailExist = await _userRepository.IsEmailExist(registerUserViewModel.Email);
            if (emailExist)
            {
                return RegisterUserResult.UserExist;
            }

            User user = new User()
            {
                FirstName = registerUserViewModel.FirstName.SanitizeText(),
                LastName = registerUserViewModel.LastName.SanitizeText(),
                EmailActiveCode = NameGenerator.GenerateUniqCode(),
                Password = PasswordHellper.EncodePasswordMd5(registerUserViewModel.Password).SanitizeText(),
                Mobile = registerUserViewModel.Mobile.SanitizeText(),
                UserState = UserState.NotActive,
                RegisterDate = DateTime.Now,
                Email = registerUserViewModel.Email,

                //UserAvatar = "Default.png",


            };

            await _userRepository.AddUser(user);
            await _userRepository.SaveChange();
            string body = _viewRender.RenderToStringAsync("ActiveEmail", user);
            SendEmail.Send(user.Email, "فعالسازی", body);

            return RegisterUserResult.Success;

        }
        public async Task<bool> CheckOtpCode(string otpCode)
        {
            return await _userRepository.CheckOtpCode(otpCode);
        }
        public async Task<User> GetUserByMobil(string mobile)
        {
            return await _userRepository.GetUserByMobil(mobile);
        }
        public async Task<User> GetUserByEmail(string email)
        {
            return await _userRepository.GetUserByEmail(email);
        }
        public async Task<ForgotPasswordResult> ForgotPassword(ForgotPasswordViewModel forgot)
        {
            var user = await _userRepository.GetUserByEmail(forgot.Email);
            if (user == null)
            {
                return ForgotPasswordResult.Error;
            }

            var resetPass = new ResetPasswordViewModel()
            {
                Password = user.Password,
                EmailActiveCode = user.EmailActiveCode
            }
                ;
            string body = _viewRender.RenderToStringAsync("_ForgotPassword", resetPass);
            SendEmail.Send(user.Email, "بازیابی کلمه عبور", body);

            _userRepository.UpdateUser(user);
            await _userRepository.SaveChange();

            return ForgotPasswordResult.Success;

        }
        public async Task<LoginUserResult> LoginUser(LoginUserViewModel loginUserViewModel)

        {
            string hashPassword = PasswordHellper.EncodePasswordMd5(loginUserViewModel.Password);
            var user = await _userRepository.GetUSerForLogin(loginUserViewModel.Email, hashPassword);
            if (user != null)
            {
                if (user.UserState == UserState.NotActive)
                    return LoginUserResult.NotActive;

                return LoginUserResult.Success;
            }
            else
            {
                return LoginUserResult.UserNotFound;
            }
        }

        public async Task<User> GetUserByActiveCode(string activeCode)
        {
            return await _userRepository.GetUserByActiveCode(activeCode);
        }

        public async Task<ResetPasswordResult> ResetPassword(ResetPasswordViewModel reset)
        {
            var user = await _userRepository.GetUserByActiveCode(reset.EmailActiveCode);


            if (user != null)

            {
                string hashNewPassword = PasswordHellper.EncodePasswordMd5(reset.Password).SanitizeText();
                user.Password = hashNewPassword;
                _userRepository.UpdateUser(user);
                await _userRepository.SaveChange();
                return ResetPasswordResult.Success;
            }
            return ResetPasswordResult.NotValid;
        }
        public async Task<ActiveEmailResult> ActiveAccount(EmailActiveAccountViewModel activeCode)
        {
            var activeEmailExist = await _userRepository.CheckEmailActiveCode(activeCode.EmailActiveCode);

            var user = await _userRepository.GetUserByActiveCode(activeCode.EmailActiveCode);

            if (user == null)
                return ActiveEmailResult.Error;


            if (activeEmailExist)
            {

                user.UserState = UserState.Active;
                user.EmailActiveCode = NameGenerator.GenerateUniqCode();
                _userRepository.UpdateUser(user);
                await _userRepository.SaveChange();
                return ActiveEmailResult.Success;

            }
            return ActiveEmailResult.NotActive;
        }

        public async Task<bool> IsExistMobileNumber(string mobileNumber)
        {
            return await _userRepository.IsExistMobileNumber(mobileNumber);
        }
        #endregion

        #region Admin
        public async Task<FilterUserViewModel> FilterUsers(FilterUserViewModel filter)
        {
            return await _userRepository.FilterUser(filter);
        }

        public async Task<UserTypeResult> CreateUser(CreateUserViewModel createUser)
        {

            if (createUser.Email != null)
            {
                if (await _userRepository.IsEmailExist(createUser.Email))
                {
                    return UserTypeResult.EmailExit;
                }
            }

            if (await IsExistMobileNumber(createUser.Mobile))
            {
                return UserTypeResult.MobileExit;
            }

            User user = new User()
            {
                FirstName = createUser.FirstName,
                LastName = createUser.LastName,
                EmailActiveCode = NameGenerator.GenerateUniqCode(),
                Password = PasswordHellper.EncodePasswordMd5(createUser.Password),
                Mobile = createUser.Mobile,
                RegisterDate = DateTime.Now,
                UserState = UserState.Active,
                IsSuperAdmin = createUser.IsSuperAdmin,

            };
            if (createUser.Email != null)
                user.Email = createUser.Email.ToLower();

            if (createUser.UserAvatar != null)
            {
                string imageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(createUser.UserAvatar.FileName);
                createUser.UserAvatar.AddImageToServer(imageName, FilePath.FilePath.UserAvatarServer, 100, 100, FilePath.FilePath.UserAvatarThumbServer);
                user.UserAvatar = imageName;
            }

            await _userRepository.CreateUser(user);
            await _userRepository.SaveChange();


            if (createUser.UserRoles != null)
            {
                await CreateUserRole(user.Id, createUser.UserRoles);
                await _userRepository.SaveChange();
            }
            return UserTypeResult.Success;


        }

        public async Task<EditUserViewModel> GetUserForEditById(long userId)
        {
            var user = await _userRepository.GetUserForEdit(userId);
            if (user == null) return null;
            return user;
        }

        public async Task<EditUserTypeResult> EditUser(EditUserViewModel editUserViewModel)
        {
            var user = await _userRepository.GetUserByUserId(editUserViewModel.UserId);
            if (user == null)
            {
                return EditUserTypeResult.NotFound;
            }

            if (user.Email != editUserViewModel.Email)
            {
                if (await _userRepository.IsEmailExist(editUserViewModel.Email))
                {
                    return EditUserTypeResult.EmailExit;
                }

            }

            if (user.Mobile != editUserViewModel.Mobile)
            {
                if (await _userRepository.IsExistMobileNumber(editUserViewModel.Mobile))
                {
                    return EditUserTypeResult.MobileExit;
                }
            }

            user.FirstName = editUserViewModel.FirstName;
            user.LastName = editUserViewModel.LastName;
            user.Email = (editUserViewModel.Email == "null" ? null : editUserViewModel.Email.ToLower());
            if (user.Password != null)
            {
                user.Password = PasswordHellper.EncodePasswordMd5(editUserViewModel.Password);
            }

            user.IsSuperAdmin = editUserViewModel.IsSuperAdmin;

            //avatar
            if (editUserViewModel.UserAvatar != null)
            {
                string image = NameGenerator.GenerateUniqCode() +
                               Path.GetExtension(editUserViewModel.UserAvatar.FileName);
                editUserViewModel.UserAvatar.AddImageToServer(image, FilePath.FilePath.UserAvatarServer, 100, 100, FilePath.FilePath.UserAvatarThumbServer);
                user.UserAvatar = image;
            }
            _userRepository.EditUser(user);
            await _userRepository.SaveChange();

            //Role (first should delete previous roles (DeleteAllUserRoles Method),
            //then create new roles for edit + save (because is in other DB)
            if (editUserViewModel.UserRoles != null)
            {
               await DeleteAllUserRoles(user.Id);
                await CreateUserRole(user.Id, editUserViewModel.UserRoles);
            }

            await _userRepository.SaveChange();
            return EditUserTypeResult.Success;
        }

        public async Task<DeleteUserResult> DeleteUser(long id)
        {
            var user = await _userRepository.GetUserByUserId(id);
            if (user == null)
            {
                return DeleteUserResult.UserNotFound;
            }

            user.IsDelete = true;
            _userRepository.EditUser(user);
            await _userRepository.SaveChange();
            return DeleteUserResult.SuccessDeleted;



        }

        public async Task<RestoreUserResult> RestoreUser(long id)
        {
            var user = await _userRepository.GetUserByUserId(id);
            if (user == null)
            {
                return RestoreUserResult.NotFound;
            }

            user.IsDelete = false;
            _userRepository.EditUser(user);
            await _userRepository.SaveChange();
            return RestoreUserResult.SuccessRestore;
        }

        public async Task CreateUserRole(long userId, List<long> selectedUserRoles)
        {
            if (!selectedUserRoles.Any()) return;
            foreach (var UserRole in selectedUserRoles)
            {
                UserRole userRole = new UserRole()
                {
                    RoleId = UserRole,
                    UserId = userId

                };
                await _userRepository.CreateRole(userRole);
                await _userRepository.SaveChange();


            }

        }

        public async Task DeleteAllUserRoles(long userId)
        {
            _userRepository.DeleteAllUserRoles(userId);
            await _userRepository.SaveChange();
        }

        #endregion
    }
}