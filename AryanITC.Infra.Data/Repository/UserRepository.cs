using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AryanITC.Domain.Entities.Account;
using AryanITC.Domain.IRepository;
using AryanITC.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace AryanITC.Infra.Data.Repository
{
    public class UserRepository : IUserRepository
    {

        #region Constructor

        private readonly AryanDbContext _context;

        public UserRepository(AryanDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Account

        public async Task<bool> IsExistMobileNumber(string mobileNumber)
        {
            return await _context.Users.AnyAsync(u => u.Mobile == mobileNumber);
        }

        public async Task<bool> IsEmailExist(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task AddUser(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<bool> CheckOtpCode(string otpCode)
        {
            return await _context.Users.AnyAsync(U => U.OtpCode == otpCode);
        }
        public async Task<User> GetUserByMobil(string mobile)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Mobile == mobile);
        }



        #endregion

        #region Loging

        public async Task<User> GetUSerForLogin(string email, string password)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Email == email && u.Password == password);
        }

        public async Task<bool> ActiveAccount(string emailActiveAccount)
        {
          return await _context.Users.AnyAsync(u => u.EmailActiveCode == emailActiveAccount);

        
        }


        #endregion

        #region SaveChange

        public async Task SaveChange()
        {
            await _context.SaveChangesAsync();
        }

        #endregion

    }
}
