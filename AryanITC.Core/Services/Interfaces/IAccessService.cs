using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AryanITC.Domain.ViewModels.Permission;
using AryanITC.Domain.ViewModels.Role;

namespace AryanITC.Core.Services.Interfaces
{
   public interface IAccessService
    {
        #region Roles

        Task<List<RoleViewModel>> GetAllRoles();
        Task<FilterRoleViewModel> FilterRoles(FilterRoleViewModel filterRoles);
        Task<RoleTypeResult> CreateRole(CreateRoleViewModel createRoleViewModel);
        Task<bool> IsRoleExistsByRoleTitle(string roleTitle);


        #endregion

        #region Pemission

        Task<List<PermissionViewModel>> GetAllPermissions();

        #endregion
    }
}
