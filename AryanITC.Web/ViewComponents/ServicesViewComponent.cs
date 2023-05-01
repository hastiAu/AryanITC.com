using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AryanITC.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AryanITC.Web.ViewComponents
{
    public class ServicesViewComponent: ViewComponent
    {
        #region Constructor

        private readonly ISiteService _siteService;

        public ServicesViewComponent(ISiteService siteService)
        {
            _siteService = siteService;
        }

        #endregion
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var serviceTitle = await _siteService.GetSiteSettingForEdit();
            ViewData["ServiceTitle"] = serviceTitle.AryanServiceTitle;

            var service = await _siteService.GetAllServiceForShowInSite();
            return await Task.FromResult((IViewComponentResult) View("Services", service));
            //return View("/Views/Shared/Components/Services/Services.cshtml");
        }
    }
}
