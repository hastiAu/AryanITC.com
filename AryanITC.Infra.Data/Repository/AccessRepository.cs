using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AryanITC.Domain.IRepository;
using AryanITC.Domain.ViewModels.Role;
using AryanITC.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AryanITC.Infra.Data.Repository
{
  public class AccessRepository:IAccessRepository
  {
        #region Constructor

        private readonly AryanDbContext _context;

        public AccessRepository(AryanDbContext context)
        {
            _context = context;
        }
        

        #endregion

        #region Save Change

        public async Task SaveChange()
        {
            await _context.SaveChangesAsync();
        }


        #endregion


        public async Task<List<RoleViewModel>> GetAllRoles()
        {
            return await _context.Roles
                .Where(r => !r.IsDelete)
                .Select(r => new RoleViewModel()
                    {
                    RoleId = r.Id,
                    RoleTitle = r.RoleTitle
                    } ).ToListAsync();

        }
    }
}
