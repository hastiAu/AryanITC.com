using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AryanITC.Domain.Entities.SiteSetting;
using AryanITC.Domain.IRepository;
using AryanITC.Domain.ViewModels.SiteSetting;
using AryanITC.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AryanITC.Infra.Data.Repository
{
   public class SiteSettingRepository: ISiteSettingRepository
    {

        #region Constructor

        private readonly AryanDbContext _context;

        public SiteSettingRepository(AryanDbContext context)
        {
            _context = context;
        }


        #endregion



        #region Save Change

        public async Task SaveChange()
        {
            await _context.SaveChangesAsync();
        }
        #endregion


        public  async Task<SiteSettingViewModel> GetSiteSettingForEdit()
        {
            var siteSetting = _context.SiteSettings
                .Select(s => new SiteSettingViewModel()
                    {
                        AboutUs = s.AboutUs,
                        Address = s.Address,
                        AryanCloudHostDescription = s.AryanCloudHostDescription,
                        AryanCloudHostTitle = s.AryanCloudHostTitle,
                        AryanDomainDescription = s.AryanDomainDescription,
                        AryanDomainTitle = s.AryanDomainTitle,
                        AryanHostPriceDescription = s.AryanHostPriceDescription,
                        AryanHostPriceTitle = s.AryanHostPriceTitle,
                        AryanPortfolioGroupName = s.AryanPortfolioGroupName,
                        AryanPortfolioTitle = s.AryanPortfolioTitle,
                        AryanServiceDescription = s.AryanServiceDescription,
                        AryanServiceTitle = s.AryanServiceTitle,
                        CopyRight = s.CopyRight,
                        SiteEmail = s.SiteEmail,
                        SiteLogo = s.SiteLogo,
                        SiteName = s.SiteName,
                        SitePhone = s.SitePhone,
                        SiteSettingId = s.Id,
                        SiteUrl = s.SiteUrl

                    }

                ).SingleOrDefaultAsync();
            return await (siteSetting);
        }

        public void UpdateSiteSetting(SiteSetting siteSetting)
        {
            _context.SiteSettings.Update(siteSetting);
        }

        public async Task<SiteSetting> GetDefaultSiteSetting()
        {
            return await _context.SiteSettings.SingleOrDefaultAsync();
        }
    }
}
