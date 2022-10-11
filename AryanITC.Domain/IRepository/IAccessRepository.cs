using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AryanITC.Domain.Entities.Access;
using AryanITC.Domain.ViewModels.Role;

namespace AryanITC.Domain.IRepository
{
  public  interface IAccessRepository
    {
        #region SaveChanges

        Task SaveChange();

        #endregion

         
        #region Role

        //Use this method for showing Roles for adminPanel and site
        Task<List<RoleViewModel>> GetAllRoles();

        #endregion
    }
}
