using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AryanITC.Core.Extensions;
using AryanITC.Core.Generator;
using AryanITC.Core.Services.Interfaces;
using AryanITC.Domain.IRepository;
using AryanITC.Domain.ViewModels.SiteSetting;

namespace AryanITC.Core.Services.Implementations
{
    public class SiteService : ISiteService

    {

        #region Costructor

        private readonly ISiteSettingRepository _siteSettingRepository;

        public SiteService(ISiteSettingRepository siteSettingRepository)
        {
            _siteSettingRepository = siteSettingRepository; 
        }

    

        #endregion

        public async Task<SiteSettingViewModel> GetSiteSettingForEdit()
        {
          return await _siteSettingRepository.GetSiteSettingForEdit();
             
        }

        public async Task<SiteSettingEditResult> UpdateSiteSetting(SiteSettingViewModel siteSettingViewModel)
        {
            var siteSetting = await _siteSettingRepository.GetDefaultSiteSetting();
            if (siteSetting == null)
            {
                return SiteSettingEditResult.NotFound;
            }

            #region Edit

            siteSetting.SiteUrl = siteSettingViewModel.SiteUrl;
            siteSetting.AboutUs = siteSettingViewModel.AboutUs;
            siteSetting.AryanServiceTitle = siteSettingViewModel.AryanServiceTitle;
            siteSetting.AryanServiceDescription = siteSettingViewModel.AryanServiceDescription;
            siteSetting.AryanPortfolioTitle = siteSettingViewModel.AryanPortfolioTitle;
            siteSetting.AryanPortfolioGroupName = siteSettingViewModel.AryanPortfolioGroupName;
            siteSetting.AryanHostPriceTitle = siteSettingViewModel.AryanHostPriceTitle;
            siteSetting.AryanHostPriceDescription = siteSettingViewModel.AryanHostPriceDescription;
            siteSetting.AryanDomainTitle = siteSettingViewModel.AryanDomainTitle;
            siteSetting.AryanDomainDescription = siteSettingViewModel.AryanDomainDescription;
            siteSetting.AryanCloudHostTitle = siteSettingViewModel.AryanCloudHostTitle;
            siteSetting.AryanCloudHostDescription = siteSettingViewModel.AryanCloudHostDescription;
            siteSetting.SiteName = siteSettingViewModel.SiteName;
            siteSetting.SitePhone = siteSettingViewModel.SitePhone;
            siteSetting.Address = siteSettingViewModel.Address;
            siteSetting.CopyRight = siteSettingViewModel.CopyRight;
            siteSetting.SiteEmail = siteSettingViewModel.SiteEmail;

            #endregion

            if (siteSettingViewModel.LogoFile != null)
            {
                string imgName= NameGenerator.GenerateUniqCode()+ Path.GetExtension(siteSettingViewModel.LogoFile.FileName);
                siteSettingViewModel.LogoFile.AddImageToServer(imgName,FilePath.FilePath.SiteSettingLogoServer,150,150);
                siteSetting.SiteLogo = imgName;
            }

            _siteSettingRepository.UpdateSiteSetting(siteSetting);
            await _siteSettingRepository.SaveChange();
            return SiteSettingEditResult.Success;
        }
    }
}
