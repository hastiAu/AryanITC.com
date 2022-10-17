using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AryanITC.Domain.Entities.Access;
using AryanITC.Domain.IRepository;
using AryanITC.Domain.ViewModels.Pagination;
using AryanITC.Domain.ViewModels.Permission;
using AryanITC.Domain.ViewModels.Role;
using AryanITC.Infra.Data.Context;
using ElmahCore;
using Microsoft.EntityFrameworkCore;

namespace AryanITC.Infra.Data.Repository
{
    public class AccessRepository : IAccessRepository
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
                }).ToListAsync();

        }

        public async Task<FilterRoleViewModel> FilterRoles(FilterRoleViewModel filterRole)
        {
            var query = _context.Roles.AsQueryable();

            #region Filter

            switch (filterRole.FilterRoleState)
            {
                case FilterRoleState.All:
                    break;

                case FilterRoleState.Deleted:
                    {
                        query = query.Where(u => u.IsDelete);
                        break;
                    }
                case FilterRoleState.NotDeleted:
                {
                    query = query.Where(u => !u.IsDelete);
                    break;
                }
            }

            if (!string.IsNullOrEmpty(filterRole.RoleTitle))
            {
                query = query.Where(u => u.RoleTitle.Contains(u.RoleTitle.ToLower()));
            }

            #endregion

            int allEntitiesCount = await query.CountAsync();
            var pager = Pagination.BuildPagination(filterRole.PageId, allEntitiesCount);
            var users = await query.OrderBy(o => o.IsDelete).Pagination(pager).ToListAsync();
            filterRole.SetRoles(users);
            return filterRole.SetPaging(pager);
        }

        public async Task<bool> IsRoleExistsByRoleTitle(string roleTitle)
        {
            return await _context.Roles.AnyAsync(u => u.RoleTitle== roleTitle.ToLower());
        }

        public async Task CreateRole(Role role)
        {
              await _context.AddAsync(role);
        }

        public async Task<Role> GetRoleById(long roleId)
        {
            return await _context.Roles.SingleOrDefaultAsync(u => u.Id == roleId);
        }

        public void EditRole(Role role)
        {
              _context.Update(role);
        }

        public async Task<bool> IsRoleNotDeleted(string roleTitle)
        {
          return await _context.Roles.AnyAsync(u => u.RoleTitle == roleTitle.ToLower());
        }

        public async Task<List<PermissionViewModel>> GetAllPermission()
        {
            return await _context.Permission
                .Select(a => new PermissionViewModel()
                {
                    ParentId = a.ParentId,
                    PermissionId = a.Id,
                    PermissionTitle = a.PermissionTitle

                }).ToListAsync();
        }

        public async Task CreateRolePermission(RolePermission rolePermission)
        {
            await _context.RolePermissions.AddAsync(rolePermission);
        }
        public void DeleteAllRolePermissions(long roleId)
        {
            _context.RolePermissions
                .Where(r => r.RoleId == roleId)
                .ToList()
                .ForEach(r => _context.RolePermissions.Remove(r));
        }

        public async Task<EditRoleViewModel> GetRoleInformationByRoleId(long roleId)
        {
            if (roleId != 0)
            {
                var role = await _context.Roles
                    .Where(u => u.Id == roleId && !u.IsDelete)
                    .Select(u => new EditRoleViewModel()
               {
                        RoleTitle = u.RoleTitle,
                        RolePermissions = u.RolePermissions.Select(u=>u.Id).ToList(),
                        RoleId = u.Id,
                        }
                    ).SingleOrDefaultAsync();
                return role;
            }

            return null;
        }
    }
}
