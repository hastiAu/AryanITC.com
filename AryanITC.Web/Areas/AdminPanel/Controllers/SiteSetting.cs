using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using AryanITC.Core.Services.Interfaces;
using AryanITC.Domain.ViewModels.SiteSetting;

namespace AryanITC.Web.Areas.AdminPanel.Controllers
{
    public class SiteSetting : BaseAdminController
    {

        #region Constructor

        private readonly ISiteService _siteService;

        public SiteSetting(ISiteService siteService)
        {
            _siteService = siteService; 
        }

        #endregion




        #region SiteSetting

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var siteSetting = await _siteService.GetSiteSettingForEdit();
            if (siteSetting == null)
            {
                return RedirectToAction("NotFound", "Home");
            }
            return View(siteSetting);
        }



        [HttpPost]
        public async Task<IActionResult> Index(SiteSettingViewModel siteSettingViewModel)
        {

            if (!ModelState.IsValid)
            {
                return View(siteSettingViewModel);
            }

            SiteSettingEditResult result = await _siteService.UpdateSiteSetting(siteSettingViewModel);

            switch (result)
            {
                case SiteSettingEditResult.NotFound:
                {
                    ViewBag.ErrorText = "در خواست شما با خطا مواجه شد";
                    return View(siteSettingViewModel);
                }

                case SiteSettingEditResult.Success:
                    ViewBag.SuccessText = "ویرایش با موفقیت انجام شد";
                    return View(siteSettingViewModel);

                   
            }
            return RedirectToAction("Index");
        }

        #endregion
    }
}
