using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AryanITC.Domain.Entities.Account;

namespace AryanITC.Domain.IRepository
{
    public interface IUserRepository
    {
        #region Account

        Task<bool> IsExistMobileNumber(string mobileNumber);
        Task<bool> IsEmailExist(string email);
        Task AddUser(User user);

        Task<bool> CheckOtpCode(string otpCode);

        Task<User> GetUserByMobil(string mobile);
        Task<User> GetUSerForLogin(string email, string password);

        Task<bool> ActiveAccount(string emailActiveAccount);

        #endregion


        #region SaveChange

        Task SaveChange();

        #endregion


    }
}
