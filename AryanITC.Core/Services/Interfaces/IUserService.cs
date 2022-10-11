using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AryanITC.Domain.Entities.Account;
using AryanITC.Domain.ViewModels.Account;
using AryanITC.Domain.ViewModels.ManagementUser;
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
        Task<bool> IsExistMobileNumber(string mobileNumber);

        #endregion

        #region Login

        Task<LoginUserResult> LoginUser(LoginUserViewModel loginUserViewModel);
        Task<ActiveEmailResult> ActiveAccount(EmailActiveAccountViewModel activeCode);
        Task<User> GetUserByActiveCode(string activeCode);
        Task<ForgotPasswordResult> ForgotPassword(ForgotPasswordViewModel forgot);
        Task<ResetPasswordResult> ResetPassword(ResetPasswordViewModel reset);

        #endregion

        #region Admin

        Task<FilterUserViewModel> FilterUsers(FilterUserViewModel filter);
        Task<UserTypeResult> CreateUser(CreateUserViewModel createUser);


        #endregion

        #region User Role

        Task CreateUserRole(long userId, List<long> selectedUserRoles);
        Task DeleteAllUserRoles(long userId);

        #endregion


    }
}
