using System;
using System.Collections.Generic;
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
using AryanITC.Domain.Entities.Account;
using AryanITC.Domain.IRepository;
using AryanITC.Domain.ViewModels.Account;
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
            
            
            string body = _viewRender.RenderToStringAsync("ActiveEmail", user );
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
                Password =  user.Password,
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
            var activeEmailExist=await _userRepository.CheckEmailActiveCode(activeCode.EmailActiveCode);

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

        #endregion
         

    }
}