using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        #region Filter About Us
       
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


        #region Create About Us

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

        #region Edit About Us

        [HttpGet]
        public async Task<IActionResult> EditAboutUs(long id)
        {
            if (id <= 0)
            {
                return NotFound();
            }
            var result = await _siteService.GetEditAboutUsForEdit(id);
            return   View(result);
        }

        [HttpPost]
        public async Task<IActionResult> EditAboutUs(EditAboutUsViewModel editAboutUsViewModel)
        {

            if (!ModelState.IsValid)
            {

                return View(editAboutUsViewModel);
            }
            var result = await _siteService.UpdateAboutUs(editAboutUsViewModel);
                switch (result)
                {
                    case EditAboutUsResult.NotFound:
                        return View("NotFound");

                    case EditAboutUsResult.Error:
                        ModelState.AddModelError("AboutUsTitle", "درخواست شما با خطا مواجه شد");
                        break;

                    case EditAboutUsResult.Success:
                        TempData["SuccessOperation"] = "ویرایش درباره ما با موفقیت انجام شد";
                    return RedirectToAction("Index");
                     
                }


                return View();
        }

        #endregion

        #region Delete About Us

        public async Task<IActionResult> DeleteAboutUs(long id)
        {
            var result = await _siteService.DeleteAboutUs(id);

            switch (result)
            {
                case DeleteAboutUsResult.AboutUsNotFound:
                    return Json(new
                    {
                        text = "درباره ما مورد نظر یافت نشد"
                    });

                case DeleteAboutUsResult.SuccessDeleted:

                    return Json(new
                    {
                        text = " درباره ما با موفقیت حذف شد",
                        statusCode = HttpStatusCode.OK
                    });
            }

            return RedirectToAction("Index");
        }

        #endregion

        #region Restore About Us

        public async Task<IActionResult> RestoreAboutUs(long id)
        {
            var result = await _siteService.RestoreAboutUs(id);

            switch (result)
            {
                case RestoreAboutUsResult.AboutUsNotFound:
                    return Json(new
                    {
                        text = "درباره ما مورد نظر یافت نشد"
                    });

                case RestoreAboutUsResult.SuccessRestored:

                    return Json(new
                    {
                        text = " درباره ما با موفقیت بازگردانی شد",
                        statusCode = HttpStatusCode.OK
                    });
            }

            return RedirectToAction("Index");
        }

        #endregion
    }
}
