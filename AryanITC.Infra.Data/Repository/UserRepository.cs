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

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
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



        public async Task<User> GetUserByActiveCode(string activeCode)
        {
            var user= await _context.Users.SingleOrDefaultAsync(u => u.EmailActiveCode == activeCode);
            return user;
        }

        public async Task<bool> CheckEmailActiveCode(string activeCode)
        {
             var active= await _context.Users.AnyAsync(u => u.EmailActiveCode == activeCode);
             return active;
        }


        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
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
