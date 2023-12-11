using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AryanITC.Core.Services.Interfaces;
using AryanITC.Domain.ViewModels.Portfolio;
using AryanITC.Infra.Data.Context;

namespace AryanITC.Web.Areas.AdminPanel.Controllers
{
    public class PortfolioController : BaseAdminController 
    {

        #region Constructor

        private readonly ISiteService _siteService;

        public PortfolioController(ISiteService service)
        {
            _siteService = service;
        }

        #endregion

        public IActionResult Index (string? successText)
        {
            ViewBag.SuccessText = successText;
            return View();
    }

        #region Create Portfolio Category
        [HttpGet]
        public IActionResult CreatePortfolioCategory()
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("NotFound");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePortfolioCategory(CreatePortfolioCategoryViewModel createPortfolioCategoryViewModel)
        {

            if (!ModelState.IsValid)
            {
                return View(createPortfolioCategoryViewModel);
            }

            var result = await _siteService.CreatePortfolioCategory(createPortfolioCategoryViewModel);
                switch (result)
                {
                    case CreatePortfolioCategoryResult.NotFound:
                        ViewBag.ErrorText = "دسته بندی نمونه کار پیدا نشد";
                        return View(createPortfolioCategoryViewModel);



                    case CreatePortfolioCategoryResult.Created:
                        ViewBag.SuccessText = "دسته بندی نمونه کار  با موفقیت اضافه شد";
                        break;
            }
           

            return RedirectToAction("Index", new { successText = ViewBag.SuccessText });
            }

     
        #endregion

    }
}
