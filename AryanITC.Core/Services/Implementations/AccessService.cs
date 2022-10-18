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
    public class AccessService : IAccessService
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

        public async Task<bool> IsRoleExistsByRoleTitle(string roleTitle)
        {
            return await _accessRepository.IsRoleExistsByRoleTitle(roleTitle);
        }

        public async Task<EditRoleViewModel> GetRoleInformationByRoleId(long roleId)
        {
            return await _accessRepository.GetRoleInformationByRoleId(roleId);
        }

        public async Task<EditRoleTypeResult> EditRole(EditRoleViewModel editRoleViewModel)
        {
            var role = await _accessRepository.GetRoleById(editRoleViewModel.RoleId);
            if (role == null) return EditRoleTypeResult.NotFound;

            var roleTitle = await _accessRepository.IsRoleExistsByRoleTitle(editRoleViewModel.RoleTitle);
            if (roleTitle) return EditRoleTypeResult.Exists;

            // above code = this code
            //if (await IsRoleExistsByRoleTitle(editRoleViewModel.RoleTitle))
            //    return EditRoleTypeResult.Exists;

            //if  is not both of above code --> then this code runs and edit
            role.RoleTitle = editRoleViewModel.RoleTitle;
            _accessRepository.EditRole(role);
            await DeleteAllRolePermissions(role.Id);
            await CreateRolePermission(role.Id, editRoleViewModel.RolePermissions);
            await _accessRepository.SaveChange();


           return EditRoleTypeResult.Success;

        }

        public  async Task<DeleteRoleResult> DeleteRole(long roleId)
        {
            Role role = await _accessRepository.GetRoleById(roleId);

            if (role == null)
            {
                return DeleteRoleResult.NotFound;
            }

            role.IsDelete = true;
             _accessRepository.EditRole(role);
             await _accessRepository.SaveChange();
             return DeleteRoleResult.Success;
        }

        public async Task<DeleteRoleResult> RestoreDeletedRole(long roleId)
        {
            Role role = await _accessRepository.GetRoleById(roleId);

            if (role == null)
            {
                return DeleteRoleResult.NotFound;
            }

            role.IsDelete = false;
            _accessRepository.EditRole(role);
            await _accessRepository.SaveChange();
            return DeleteRoleResult.Success;
        }

        public async Task<List<long>> GetRolePermission(long roleId)
        {
            return await _accessRepository.GetRolePermission(roleId);
        }

        public async Task<List<PermissionViewModel>> GetAllPermissions()
        {
            return await _accessRepository.GetAllPermission();
        }

        public async Task CreateRolePermission(long roleId, List<long> selectedRolePermissions)
        {
            if (!selectedRolePermissions.Any()) return;
            foreach (int selectedRolePermission in selectedRolePermissions)
            {
                RolePermission RoleSelectedCategory = new RolePermission()
                {
                    RoleId = roleId,
                    PermissionId = selectedRolePermission
                };
                await _accessRepository.CreateRolePermission(RoleSelectedCategory);
                await _accessRepository.SaveChange();
            }
        }
        public async Task DeleteAllRolePermissions(long roleId)
        {
            _accessRepository.DeleteAllRolePermissions(roleId);
            await _accessRepository.SaveChange();
        }
    }
}
