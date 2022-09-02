using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AryanITC.Core.Generator;
using AryanITC.Core.Services.Interfaces;
using AryanITC.Domain.ViewModels.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;
using static AryanITC.Domain.ViewModels.Account.LoginUserViewModel;
 

namespace AryanITC.Web.Controllers
{
    public class AccountController : SiteBaseController
    {

        #region Constructor

        private readonly IUserService _userService;
        private readonly IStringLocalizer<AccountController> _localizer;

        public AccountController(IUserService userService, IStringLocalizer<AccountController> localizer)
        {
            _userService = userService;
            _localizer = localizer; 
        }
        #endregion

        #region RegisterUser

     
        //[Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost( )]
        //[HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterUserViewModel registerUserViewModel)
        {
            if (ModelState.IsValid)

            {
                var result= await _userService.RegisterUser(registerUserViewModel);

                switch (result)
                {
                    case RegisterUserResult.Error:
                        ModelState.AddModelError(string.Empty, "شکست");
                        TempData[ErrorMessage] = "شکست";
                        break;

                    case RegisterUserResult.UserExist:
                        ModelState.AddModelError("Email", "بجه قبلا ثبت نام کردید");
                        TempData[WarningMessage] = "بچه جون قبلا ثبت نام کردید";

                        break;

                    case RegisterUserResult.Success:
                        //TempData["Email"] = registerUserViewModel.Email;
                        TempData[SuccessMessage] = "با موفقیت ثبت نام کردید";
                        //return RedirectToAction("Index", "Home");
                        break

                        ;
 


                }

            }
            return View(registerUserViewModel);
        }

        #endregion

        //#region Login

        //public IActionResult Login()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Login(LoginUserViewModel loginUserViewModel)
        //{ 

        //    if (ModelState.IsValid)
        //    {
        //        var result = await _userService.LoginUser(loginUserViewModel);

        //        switch (result)
        //        {
        //            case LoginUserResult.Error:
        //            {
        //                ModelState.AddModelError("Email","کاربر عزیز ایمیل وارد شده اشتباه است");
        //                break;
        //            }

        //            case LoginUserResult.NotActive:
        //            {
        //                ModelState.AddModelError("Email", "حساب کاربری شما هنوز فعال نشده است");
        //                TempData[InfoMessage] = "حساب کاربری شما هنوز فعال نشده است";
        //                    break;
        //                }

        //            case LoginUserResult.UserNotFound:
        //            {
        //                ModelState.AddModelError("Email", "کاربر مورد نظر یافت نشد ");
        //                break;
        //                }

        //            case LoginUserResult.Success:
        //            {
        //                ModelState.AddModelError("Email", "ورود با موفقیت انجام شد");
        //                ViewBag.IsSuccess = true;
        //                return View();
                            
        //            }
        //        }
        //    }

           
        //    return View(loginUserViewModel);
        //}
        //#endregion

        //#region SuccessRegister  

        ////[HttpGet("active-Account")]
        ////public IActionResult SuccessRegister()
        ////{
        ////    ViewBag.email = TempData["Email"];

        ////    if (TempData["CheckOtpCode"] == null)
        ////    {
        ////        return RedirectToAction("Register", "Account");

        ////    }
        ////    return View();

        ////}

        //#endregion

        //#region ActiveEmailAccount


        //public IActionResult ActiveEmailAccount( string activeEmailAccount)
        //{

        //    return View();
        //}

        //#endregion


    }
}
