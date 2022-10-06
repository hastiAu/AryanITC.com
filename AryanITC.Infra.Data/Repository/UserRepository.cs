using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AryanITC.Domain.Entities.Account;
using AryanITC.Domain.IRepository;
using AryanITC.Domain.ViewModels.ManagementUser;
using AryanITC.Domain.ViewModels.Pagination;
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


        //Login
        public async Task<User> GetUSerForLogin(string email, string password)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Email == email && u.Password == password);
        }



        public async Task<User> GetUserByActiveCode(string activeCode)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.EmailActiveCode == activeCode);
            return user;
        }


        public async Task<bool> CheckEmailActiveCode(string activeCode)
        {
            var active = await _context.Users.AnyAsync(u => u.EmailActiveCode == activeCode);
            return active;
        }


        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
        }



        #endregion


        #region AdminPanel


        public async Task<FilterUserViewModel> FilterUser(FilterUserViewModel filter)
        {
            var query = _context.Users.AsQueryable();

            #region fFilter

            switch (filter.FilterUserState)
            {
                case FilterUserState.All:
                    break;
                case FilterUserState.Active:
                {
                    query = query.Where(u => u.UserState == UserState.Active);
                    break;
                }
                case FilterUserState.Deactivate:
                {
                    query = query.Where(u => u.UserState != UserState.Active);
                    break;
                }
                case FilterUserState.Banned:
                {

                    query = query.Where(u => u.UserState == UserState.Ban);
                    break;
                }
                case FilterUserState.Deleted:
                {
                    query = query.Where(u => u.IsDelete);
                    break;
                }
            }

            if (!string.IsNullOrEmpty(filter.Email))
            {
                query = query.Where(u => u.Email.ToLower().Contains(filter.Email.ToLower()));
            }

            if (!string.IsNullOrEmpty(filter.FullName))
            {
                query = query.Where(u => u.FirstName.ToLower().Contains(filter.FullName.ToLower()) || u.LastName.ToLower().Contains(filter.FullName.ToLower()));
            }

            if (!string.IsNullOrEmpty(filter.MobileNumber))
            {
                query=query.Where(u=>u.Mobile.Contains(filter.MobileNumber));

            }
            #endregion

            int allEntitiesCount = await query.CountAsync();
            var pager = Pagination.BuildPagination(filter.PageId, allEntitiesCount);
            var users = await query.OrderBy(o => o.IsDelete).Pagination(pager).ToListAsync();
            filter.SetUsers(users);
            return filter.SetPaging(pager);
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
