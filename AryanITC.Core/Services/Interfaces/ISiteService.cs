using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AryanITC.Domain.ViewModels.SiteSetting;

namespace AryanITC.Core.Services.Interfaces
{
   public interface ISiteService
    {
        Task<SiteSettingViewModel> GetSiteSettingForEdit();
        Task<SiteSettingEditResult> UpdateSiteSetting(SiteSettingViewModel siteSettingViewModel);
    }
}
