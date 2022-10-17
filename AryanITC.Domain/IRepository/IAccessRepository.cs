using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AryanITC.Domain.Entities.Access;
using AryanITC.Domain.ViewModels.Permission;
using AryanITC.Domain.ViewModels.Role;

namespace AryanITC.Domain.IRepository
{
  public  interface IAccessRepository
    {
        #region SaveChanges

        Task SaveChange();

        #endregion

         
        #region Role

        //Use this method for showing Roles for users adminPanel and site
        Task<List<RoleViewModel>> GetAllRoles();
        Task<FilterRoleViewModel> FilterRoles(FilterRoleViewModel filterRole);
        Task<bool> IsRoleExistsByRoleTitle(string roleTitle);
        Task CreateRole(Role role);
        Task<EditRoleViewModel> GetRoleInformationByRoleId(long roleId);
        Task<Role> GetRoleById(long roleId);
        void EditRole(Role role);
        Task<bool> IsRoleNotDeleted(string roleTitle);
        void DeleteAllRolePermissions(long roleId);
        Task CreateRolePermission(RolePermission rolePermission);

        #endregion

        #region Permission

        Task<List<PermissionViewModel>> GetAllPermission();

        #endregion
    }
}
