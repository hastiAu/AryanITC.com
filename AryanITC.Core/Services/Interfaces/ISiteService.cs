using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AryanITC.Domain.Entities.Service;
using AryanITC.Domain.ViewModels.AboutUs;
using AryanITC.Domain.ViewModels.Service;
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
        Task<DeleteAboutUsResult> DeleteAboutUs(long aboutUsId);
        Task<RestoreAboutUsResult> RestoreAboutUs(long aboutUsId);
        Task<List<AboutUsViewModel>> GetAllAboutUsForShowInSite();

        #endregion

        #region Service

        Task<FilterServiceViewModel> FilterAdminService(FilterServiceViewModel filterServiceViewModel);
        Task<CreateServiceResult> CreateService(CreateServiceViewModel createServiceViewModel);
        Task<List<ServiceViewModel>> GetAllServiceForShowInSite();


        #endregion

    }
}
