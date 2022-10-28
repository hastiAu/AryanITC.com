using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AryanITC.Domain.ViewModels.AboutUs;
using AryanITC.Domain.ViewModels.SiteSetting;

namespace AryanITC.Core.Services.Interfaces
{
   public interface ISiteService
    {

        #region Site Setting
        Task<SiteSettingViewModel> GetSiteSettingForEdit();
        Task<SiteSettingEditResult> UpdateSiteSetting(SiteSettingViewModel siteSettingViewModel);
        #endregion

        #region About Us

        Task<FilterAboutUsViewModel> FilterAdminAboutUs(FilterAboutUsViewModel filterAboutUsViewModel);
        Task<CreateAboutUsResult> CreateAboutUs(CreateAboutUsViewModel createAboutUsViewModel);
        Task<EditAboutUsViewModel> GetEditAboutUsForEdit(long aboutUsId);
        Task<EditAboutUsResult> UpdateAboutUs(EditAboutUsViewModel editAboutUsViewModel);

        #endregion

    }
}
