using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AryanITC.Domain.Entities.SiteSetting;
using AryanITC.Domain.ViewModels.SiteSetting;

namespace AryanITC.Domain.IRepository
{
   public interface ISiteSettingRepository
    {
        #region SaveChanges

        Task SaveChange();

        #endregion

        Task<SiteSettingViewModel> GetSiteSettingForEdit();
        void UpdateSiteSetting(SiteSetting siteSetting);
        Task<SiteSetting> GetDefaultSiteSetting();
    }
}
