using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AryanITC.Core.Extensions;
using AryanITC.Core.Generator;
using AryanITC.Core.Services.Interfaces;
using AryanITC.Domain.Entities.AboutUs;
using AryanITC.Domain.Entities.Service;
using AryanITC.Domain.IRepository;
using AryanITC.Domain.ViewModels.AboutUs;
using AryanITC.Domain.ViewModels.Service;
using AryanITC.Domain.ViewModels.SiteSetting;

namespace AryanITC.Core.Services.Implementations
{
    public class SiteService : ISiteService

    {

        #region Costructor

        private readonly ISiteSettingRepository _siteSettingRepository;
        private readonly IAboutUsRepository _aboutUsRepository;
        private readonly IServiceRepository _serviceRepository;

        public SiteService(ISiteSettingRepository siteSettingRepository, IAboutUsRepository aboutUsRepository, IServiceRepository serviceRepository)
        {
            _siteSettingRepository = siteSettingRepository;
            _aboutUsRepository = aboutUsRepository;
            _serviceRepository = serviceRepository;
        }


        #endregion

        #region SiteSetting


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
                string imgName = NameGenerator.GenerateUniqCode() + Path.GetExtension(siteSettingViewModel.LogoFile.FileName);
                siteSettingViewModel.LogoFile.AddImageToServer(imgName, FilePath.FilePath.SiteSettingLogoServer, 150, 150);
                siteSetting.SiteLogo = imgName;
            }

            _siteSettingRepository.UpdateSiteSetting(siteSetting);
            await _siteSettingRepository.SaveChange();
            return SiteSettingEditResult.Success;
        }
        #endregion

        #region About US

        public async Task<FilterAboutUsViewModel> FilterAdminAboutUs(FilterAboutUsViewModel filterAboutUsViewModel)
        {
            return await _aboutUsRepository.FilterAdminAboutUs(filterAboutUsViewModel);
        }

        public async Task<CreateAboutUsResult> CreateAboutUs(CreateAboutUsViewModel createAboutUsViewModel)
        {

            if (createAboutUsViewModel == null)
            {
                return CreateAboutUsResult.NotFound;
            }

            AboutUs about = new AboutUs()
            {
                AboutUsTitle = createAboutUsViewModel.AboutUsTitle,
                AboutUsDescription = createAboutUsViewModel.AboutUsDescription,
                AboutUsLink = createAboutUsViewModel.AboutUsLink,
                IsActive = createAboutUsViewModel.IsActive,
                IsDelete = createAboutUsViewModel.IsDelete,

            };

            if (createAboutUsViewModel.AboutUsImage != null)
            {
                string imageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(createAboutUsViewModel.AboutUsImage.FileName);
                createAboutUsViewModel.AboutUsImage.AddImageToServer(imageName, FilePath.FilePath.AboutUsServer, 100, 100, FilePath.FilePath.AboutUsThumbServer);
                about.AboutUsImage = imageName;
            }


            await _aboutUsRepository.CreateAboutUs(about);
            await _aboutUsRepository.SaveChange();

            return CreateAboutUsResult.Success;
        }

        public async Task<EditAboutUsViewModel> GetEditAboutUsForEdit(long aboutUsId)
        {
            return await _aboutUsRepository.GetEditAboutUsForEdit(aboutUsId);
        }

        public async Task<EditAboutUsResult> UpdateAboutUs(EditAboutUsViewModel editAboutUsViewModel)
        {
            var aboutUs = await _aboutUsRepository.GetAboutUsById(editAboutUsViewModel.AboutUsId);

            if (aboutUs == null)
            {
                return EditAboutUsResult.NotFound;
            }

            aboutUs.AboutUsTitle = editAboutUsViewModel.AboutUsTitle;
            aboutUs.AboutUsDescription = editAboutUsViewModel.AboutUsDescription;
            aboutUs.AboutUsLink = editAboutUsViewModel.AboutUsLink;
            aboutUs.IsActive = editAboutUsViewModel.IsActive;
            aboutUs.IsDelete = editAboutUsViewModel.IsDelete;

            if (aboutUs.AboutUsImage != null)
            {
                string imageName = NameGenerator.GenerateUniqCode() +
                                   Path.GetExtension(editAboutUsViewModel.AboutUsImageFile.FileName);
                editAboutUsViewModel.AboutUsImageFile.AddImageToServer(imageName, FilePath.FilePath.AboutUsServer, 100, 100, FilePath.FilePath.AboutUsThumbServer);
                aboutUs.AboutUsImage = imageName;
            }
            else
            {
                aboutUs.AboutUsImage = null;
            }

            await _aboutUsRepository.SaveChange();
            return EditAboutUsResult.Success;

        }


        public async Task<DeleteAboutUsResult> DeleteAboutUs(long aboutUsId)
        {
            var aboutUs = await _aboutUsRepository.GetAboutUsById(aboutUsId);

            if (aboutUs == null)
            {
                return DeleteAboutUsResult.AboutUsNotFound;
            }

            aboutUs.IsDelete = true;
            _aboutUsRepository.UpdateAboutUs(aboutUs);
            await _aboutUsRepository.SaveChange();
            return DeleteAboutUsResult.SuccessDeleted;

        }

        public async Task<RestoreAboutUsResult> RestoreAboutUs(long aboutUsId)
        {
            var aboutUs = await _aboutUsRepository.GetAboutUsById(aboutUsId);

            if (aboutUs == null)
            {
                return RestoreAboutUsResult.AboutUsNotFound;
            }

            aboutUs.IsDelete = false;
            _aboutUsRepository.UpdateAboutUs(aboutUs);
            await _aboutUsRepository.SaveChange();
            return RestoreAboutUsResult.SuccessRestored;
        }

        public async Task<List<AboutUsViewModel>> GetAllAboutUsForShowInSite()
        {
            return await _aboutUsRepository.GetAllAboutUsForShowInSite();
        }



        #endregion





        #region Service

        public async Task<FilterServiceViewModel> FilterAdminService(FilterServiceViewModel filterServiceViewModel)
        {
            return await _serviceRepository.FilterAdminService(filterServiceViewModel);

        }

        public async Task<CreateServiceResult> CreateService(CreateServiceViewModel createServiceViewModel)
        {
            if (createServiceViewModel == null)
            {
                return CreateServiceResult.NotFound;
            }

            Service service = new Service()
            {
                ServiceTitle = createServiceViewModel.ServiceTitle,
                ServiceDescription = createServiceViewModel.ServiceDescription,
                IsActive = createServiceViewModel.IsActive,
                IsDelete = createServiceViewModel.IsDelete,
                FontAwesome = createServiceViewModel.FontAwesome,
                ServiceLink = createServiceViewModel.ServiceLink,
                FontAwesomeColor = createServiceViewModel.FontAwesomeColor,


            };



            if (createServiceViewModel.ServiceImageFile != null)
            {
                string imageName = NameGenerator.GenerateUniqCode() +
                                   Path.GetExtension(createServiceViewModel.ServiceImageFile.FileName);

                createServiceViewModel.ServiceImageFile.AddImageToServer(imageName, FilePath.FilePath.ServiceServer, 100,
                    100, FilePath.FilePath.ServiceThumbServer);
                service.ServiceImage = imageName;
            }

            await _serviceRepository.CreateService(service);
            await _serviceRepository.SaveChange();

            return CreateServiceResult.Success;
        }

        public async Task<List<ServiceViewModel>> GetAllServiceForShowInSite()
        {
            return await _serviceRepository.GetAllServiceForShowInSite();
        }

        public async Task<EditServiceViewModel> GetServiceForEdit(long serviceId)
        {
            return await _serviceRepository.GetServiceForEdit(serviceId);
        }

        public async Task<EditServiceResult> UpdateService(EditServiceViewModel editServiceViewModel)
        {
            var service = await _serviceRepository.GetServiceById(editServiceViewModel.serviceId);
            if (service == null)
            {
                return EditServiceResult.NotFound;
            }

            service.FontAwesome = editServiceViewModel.FontAwesome;
            service.FontAwesomeColor = editServiceViewModel.FontAwesomeColor;
            service.IsActive = editServiceViewModel.IsActive;
            service.IsDelete = editServiceViewModel.IsDelete;
            service.ServiceDescription = editServiceViewModel.ServiceDescription;
            service.ServiceLink = editServiceViewModel.ServiceLink;
            service.ServiceTitle = editServiceViewModel.ServiceTitle;

            if (service.ServiceImage != null)
            {
                string imageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(editServiceViewModel.ServiceImageFile.FileName);
                editServiceViewModel.ServiceImageFile.AddImageToServer(imageName, FilePath.FilePath.ServiceServer, 100, 100,
                    FilePath.FilePath.ServiceThumbServer);
                service.ServiceImage = imageName;
            }

            else
            {
                service.ServiceImage = null;
            }

            await _serviceRepository.SaveChange();
            return EditServiceResult.Success;

            #endregion
        }

        public async Task<DeleteServiceViewModel> DeleteService(long serviceId)
        {
            var service = await _serviceRepository.GetServiceById(serviceId);

            if (service == null)
            {
                return DeleteServiceViewModel.ServiceNotFound;
            }

            service.IsDelete = true;
            _serviceRepository.UpdateService(service);
             await _serviceRepository.SaveChange();
            return DeleteServiceViewModel.SuccessDeleted;
        }

        public  async Task<RestoreServiceViewModel> RestoreService(long serviceId)
        {
            var service = await _serviceRepository.GetServiceById(serviceId);

            if (service == null)
            {
                return RestoreServiceViewModel.ServiceNotFound;
            }

            service.IsDelete = false;
            _serviceRepository.UpdateService(service);
            await _serviceRepository.SaveChange();
            return RestoreServiceViewModel.SuccessRestored;
        }
    }


}

