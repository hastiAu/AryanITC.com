using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AryanITC.Core.Services.Interfaces;
using AryanITC.Domain.ViewModels.AboutUs;

namespace AryanITC.Web.Areas.AdminPanel.Controllers
{
    public class AboutUsController : BaseAdminController
    {

        #region Constructor

        private readonly ISiteService _siteService;

        public AboutUsController(ISiteService siteService)
        {
            _siteService = siteService; 
        }

        #endregion

        #region Filter AboutUs
       
        public async Task<IActionResult> Index(FilterAboutUsViewModel filterAboutUsViewModel)
        {

            var aboutUs = await _siteService.FilterAdminAboutUs(filterAboutUsViewModel);

            //if (aboutUs == null)
            //{
            //    return RedirectToAction("NotFound", "Home");
            //}
            return View(aboutUs);


        }
 
        #endregion


        #region Create AboutUs

        [HttpGet]
        public  IActionResult CreateAboutUs()
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("NotFound");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAboutUs(CreateAboutUsViewModel createAboutUsView)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("NotFound");
            }

            else
            {
                var result = await _siteService.CreateAboutUs(createAboutUsView);

                switch (result)
                {
                    case CreateAboutUsResult.Error:
                        ViewBag.ErrorText = "در خواست شما با خطا مواجه شد";
                        return View(createAboutUsView);


                    case CreateAboutUsResult.NotFound:
                        return RedirectToAction("NotFound");

                    case CreateAboutUsResult.Success:
                        ViewBag.SuccessText = "درباره ما با موفقیت اضافه شد";
                        return RedirectToAction("Index");

                }
            }

            return View(createAboutUsView);
        }

        #endregion
    }
}
