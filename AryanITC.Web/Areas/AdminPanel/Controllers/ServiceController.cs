﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AryanITC.Core.Services.Interfaces;
using AryanITC.Domain.ViewModels.Service;

namespace AryanITC.Web.Areas.AdminPanel.Controllers
{
    public class ServiceController : BaseAdminController
    {

        #region Constructor

        private readonly ISiteService _siteService;

        public ServiceController(ISiteService siteService)
        {
            _siteService = siteService;
        }

        #endregion

        #region FilterService
        [HttpGet]
        public async Task<IActionResult> Index(FilterServiceViewModel filterServiceViewModel)
        {
            if (filterServiceViewModel == null)
            {
                return NotFound();
            }
            var service = await _siteService.FilterAdminService(filterServiceViewModel);
            return View(service);
        }

        #endregion

        #region Create


        [HttpGet]
        public IActionResult CreateService()
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("NotFound");
            }

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateService(CreateServiceViewModel createServiceViewModel)
        {
            if (ModelState.IsValid)
            {

                var result = await _siteService.CreateService(createServiceViewModel);

                switch (result)
                {
                    case CreateServiceResult.NotFound:

                        return RedirectToAction("NotFound");

                    case CreateServiceResult.Error:
                        ViewBag.ErrorText = "در خواست شما با خطا مواجه شد";
                        return View(createServiceViewModel);
                    case CreateServiceResult.Success:
                        ViewBag.SuccessText = "سرویس  با موفقیت اضافه شد";
                        return RedirectToAction("Index");
                }

            }

            return View(createServiceViewModel);

        }
        #endregion

        #region Edit Service

        [HttpGet]
        public IActionResult UpdateService(long id)
        {
            return View();
        }

        #endregion
    }
}