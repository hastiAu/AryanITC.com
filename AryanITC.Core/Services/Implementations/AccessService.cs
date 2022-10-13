using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AryanITC.Core.Services.Interfaces;
using AryanITC.Domain.Entities.Access;
using AryanITC.Domain.IRepository;
using AryanITC.Domain.ViewModels.Permission;
using AryanITC.Domain.ViewModels.Role;

namespace AryanITC.Core.Services.Implementations
{
   public class AccessService:IAccessService
   {
        #region Constructor

        private readonly IAccessRepository _accessRepository;

        public AccessService(IAccessRepository accessRepository)
        {
            _accessRepository = accessRepository;   
        }

        #endregion


        public async Task<List<RoleViewModel>> GetAllRoles()
        {
            return await _accessRepository.GetAllRoles();
        }

        public async Task<FilterRoleViewModel> FilterRoles(FilterRoleViewModel filterRoles)
        {
          return await _accessRepository.FilterRoles(filterRoles);
       


        }

        public async Task<RoleTypeResult> CreateRole(CreateRoleViewModel createRoleViewModel)
        {
            if (createRoleViewModel.RoleTitle == null)
            {
                return RoleTypeResult.NotFound;
            }

            var roleTitleIsExit = await _accessRepository.IsRoleExistsByRoleTitle(createRoleViewModel.RoleTitle);
            if (roleTitleIsExit)
            {
                return RoleTypeResult.Exists;
            }

            Role role = new Role()
            {
                RoleTitle = createRoleViewModel.RoleTitle.ToLower(),
            };

            await _accessRepository.CreateRole(role);
            await _accessRepository.SaveChange();

            //permission

            return RoleTypeResult.Success;


        }

        public  async Task<bool> IsRoleExistsByRoleTitle(string roleTitle)
        {
            return await _accessRepository.IsRoleExistsByRoleTitle(roleTitle);
        }

        public async Task<List<PermissionViewModel>> GetAllPermissions()
        {
            return await _accessRepository.GetAllPermission();
        }
   }
}
