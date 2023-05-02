using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AryanITC.Domain.Entities.AboutUs;
using AryanITC.Domain.ViewModels.AboutUs;

namespace AryanITC.Domain.IRepository
{
  public interface IAboutUsRepository
    {


        #region SaveChanges

        Task SaveChange();

        #endregion

        #region About Us

        Task<FilterAboutUsViewModel> FilterAdminAboutUs(FilterAboutUsViewModel filterAboutUsViewModel);
        Task CreateAboutUs(AboutUs aboutUs);
        Task<EditAboutUsViewModel> GetEditAboutUsForEdit(long aboutUsId);
        void UpdateAboutUs(AboutUs aboutUs);
        Task<AboutUs> GetAboutUsById(long aboutUsId);
        Task<List<AboutUsViewModel>> GetAllAboutUsForShowInSite();
        #endregion
    }
}
