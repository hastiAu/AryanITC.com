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
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using static AryanITC.Domain.ViewModels.Account.LoginUserViewModel;
using static AryanITC.Domain.ViewModels.Account.EmailActiveAccountViewModel;


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

            //*******Add It after Active SMS Panel

            //var mobileExist = await _userRepository.IsExistMobileNumber(registerUserViewModel.Mobile);
            ////var otpCode = await SendRandomNumber();

            //if (mobileExist)
            //{
            //    return RegisterUserResult.UserExist;
            //}


        
            var emailExist = await _userRepository.IsEmailExist(registerUserViewModel.Email);
            if (emailExist)
            {
                return RegisterUserResult.UserExist;
            }

            User user = new User()
            {
                //EmailActiveCode = NameGenerator.GenerateUniqCode(),
                FirstName = registerUserViewModel.FirstName.SanitizeText(),
                LastName = registerUserViewModel.LastName.SanitizeText(),
                //OtpCode = otpCode,
                EmailActiveCode = NameGenerator.GenerateUniqCode(),
                Password = PasswordHellper.EncodePasswordMd5(registerUserViewModel.Password).SanitizeText(),
                Mobile = registerUserViewModel.Mobile.SanitizeText(),
                UserState = UserState.NotActive,
                RegisterDate = DateTime.Now,
                OtpExpireTime = DateTime.Now.AddMinutes(2),
                Email = registerUserViewModel.Email,
                //UserAvatar = "Default.png",
            


            };

            await  _userRepository.AddUser(user);
            await _userRepository.SaveChange();
            //SendOtpCode(user.Mobile, user.OtpCode);
            string body = _viewRender.RenderToStringAsync("SuccessRegister", registerUserViewModel);
            SendEmail.Send(registerUserViewModel.Email, "فعالسازی", body);
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



        #region Send otpCode

        //public bool SendOtpCode(string mobile, string otpCode)
        //{
        //    var result = Senders.SendVerificationCodeSms.SendVerificationCode(mobile, otpCode);
        //    return false;
        //}


        #endregion

        #region Send RandomNumber


        //public async Task<string> SendRandomNumber()
        //{
        //var otpCode = RandomNumber.Random(100000, 999999).ToString();

        //    while (await _userRepository.CheckOtpCode(otpCode))
        //{
        //    otpCode = RandomNumber.Random(100000, 999999).ToString();
        //}
        //return otpCode;
        //}

        #endregion


    }
}