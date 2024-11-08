﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using AryanITC.Domain.Entities.Service;
using AryanITC.Domain.ViewModels.AboutUs;
using AryanITC.Domain.ViewModels.Portfolio;
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
        Task<EditServiceViewModel> GetServiceForEdit(long serviceId);
        Task<EditServiceResult> UpdateService(EditServiceViewModel editServiceViewModel);
        Task<DeleteServiceViewModel> DeleteService(long serviceId);
        Task<RestoreServiceViewModel> RestoreService(long serviceId);


        #endregion

        #region Portfolio

        Task<CreatePortfolioCategoryResult> CreatePortfolioCategory(
            CreatePortfolioCategoryViewModel createPortfolioCategoryViewModel);

 

        #endregion
    }
}
