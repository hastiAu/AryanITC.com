﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AryanITC.Domain.Entities.Account;
using AryanITC.Domain.ViewModels.Account;
using static AryanITC.Domain.ViewModels.Account.LoginUserViewModel;
using static AryanITC.Domain.ViewModels.Account.EmailActiveAccountViewModel;



namespace AryanITC.Core.Services.Interfaces
{
   public interface IUserService
    {
        #region Account

        Task<RegisterUserResult> RegisterUser(RegisterUserViewModel registerUserViewModel);
        Task<bool> CheckOtpCode(string otpCode);
        Task<User> GetUserByMobil(string mobile);
        Task<User> GetUserByEmail(string email);
       
        #endregion

        #region Login
 
        Task<LoginUserResult> LoginUser(LoginUserViewModel loginUserViewModel);
        Task<ActiveEmailResult> ActiveAccount(EmailActiveAccountViewModel activeCode);
        Task<User> GetUserByActiveCode(string activeCode);
        Task<ForgotPasswordResult> ForgotPassword(ForgotPasswordViewModel forgot);
        Task<ResetPasswordResult> ResetPassword(ResetPasswordViewModel reset);

        #endregion






    }
}
