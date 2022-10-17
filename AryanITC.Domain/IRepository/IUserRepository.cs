using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AryanITC.Domain.Entities.Access;
using AryanITC.Domain.Entities.Account;
using AryanITC.Domain.ViewModels.Account;
using AryanITC.Domain.ViewModels.ManagementUser;

namespace AryanITC.Domain.IRepository
{
    public interface IUserRepository
    {
        #region Account

        Task<bool> IsExistMobileNumber(string mobileNumber);
        Task<bool> IsEmailExist(string email);
        Task AddUser(User user);
        void UpdateUser(User user);
        Task<bool> CheckOtpCode(string otpCode);
        Task<User> GetUserByMobil(string mobile);
        Task<User> GetUSerForLogin(string email, string password);
        Task<bool> CheckEmailActiveCode(string activeCode);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByActiveCode(string activeCode);

        #endregion

        #region AdminPanel

        Task<FilterUserViewModel> FilterUser (FilterUserViewModel filter);
        Task CreateUser(User user);
        Task<User> GetUserByUserId(long userId);
        Task<EditUserViewModel> GetUserForEdit(long userId);
        void EditUser(User user);
   


        #endregion

        #region User Roles

        Task CreateRole(UserRole userRole);
        void DeleteAllUserRoles(long userId);

            #endregion

        #region SaveChange

        Task SaveChange();

        #endregion


    }
}
