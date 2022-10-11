using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AryanITC.Core.Services.Interfaces;
using AryanITC.Domain.IRepository;
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
   }
}
