using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AryanITC.Domain.Entities.Service;
using AryanITC.Domain.ViewModels.Service;

namespace AryanITC.Domain.IRepository
{
   public interface IServiceRepository

    {
        #region SaveChanges

        Task SaveChange();

        #endregion

        #region Service

        Task<FilterServiceViewModel> FilterAdminService(FilterServiceViewModel filterServiceViewModel);
        Task CreateService(Service service);
        Task<List<ServiceViewModel>> GetAllServiceForShowInSite();

        #endregion

    }
}
