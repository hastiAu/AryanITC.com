using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AryanITC.Domain.ViewModels.Role;

namespace AryanITC.Core.Services.Interfaces
{
   public interface IAccessService
    {
        #region Roles

        Task<List<RoleViewModel>> GetAllRoles();

        #endregion
    }
}
