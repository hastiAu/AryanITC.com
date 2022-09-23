using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AryanITC.Core.Convertor;
using AryanITC.Core.Generator;
using AryanITC.Core.Services.Interfaces;
using AryanITC.Domain.ViewModels.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Localization;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using static AryanITC.Domain.ViewModels.Account.LoginUserViewModel;


namespace AryanITC.Web.Controllers
{
    public class AccountController : SiteBaseController
    {

        #region Constructor

        private readonly IUserService _userService;
        private readonly IStringLocalizer<AccountController> _localizer;


        public AccountController(IUserService userService, IStringLocalizer<AccountController> localizer )
        {
            _userService = userService;
            _localizer = localizer;
            
        }
      

        #endregion

        #region RegisterUser


        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost()]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterUserViewModel registerUserViewModel)
        {
            if (ModelState.IsValid)

            {       
                var result = await _userService.RegisterUser(registerUserViewModel);

                switch (result)
                {
                    case RegisterUserResult.Error:
                        ModelState.AddModelError(string.Empty, "کاربر گرامی،ثبت نام شما با خطا مواجه شد.");

                        TempData[ErrorMessage] = "کاربر گرامی،ثبت نام شما با خطا مواجه شد.";
                        break;

                    case RegisterUserResult.UserExist:
                        ModelState.AddModelError("Email", "کاربر گرامی، شما قبلا با ایمیل ثبت نام کرده اید.");
                        TempData[WarningMessage] = "کاربر گرامی، شما قبلا با ایمیل ثبت نام کرده اید.";
                        return View(registerUserViewModel);
                      

                    case RegisterUserResult.Success:
                        TempData[SuccessMessage] = "کاربر گرامی،ثبت نام شما قبلا با موفقیت انجام شد.";


                        var user = await _userService.GetUserByEmail(registerUserViewModel.Email);
                        var activeCode = user.EmailActiveCode;

                        ViewBag.active = activeCode;
                        return View("SuccessRegister",registerUserViewModel);
 
                }

            }
             

            return View(registerUserViewModel);
        }

        #endregion

        #region Login

        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginUserViewModel loginUserViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginUser(loginUserViewModel);

                switch (result)
                {
                    case LoginUserResult.Error:
                    {
                        ModelState.AddModelError("Email", "خطا داد");
                        break;
                    }

                    case LoginUserResult.NotActive:
                    {
                        ModelState.AddModelError("Email", "کاریر فعال نیست  ");
                        break;
                    }
                    case LoginUserResult.UserNotFound:
                    {
                        ModelState.AddModelError("Email", "کاربر پیدا نشد");
                        break;

                    }
                    case LoginUserResult.Success:

                    {

                        ////ModelState.AddModelError("Email", "ورود با موفقیت انجام شد");
                        ViewBag.IsSuccess = true;
                        //TempData[SuccessMessage] = "ورود موفق";

                        var email = loginUserViewModel.Email;
                        var user = await _userService.GetUserByEmail(email);

                        var claims = new List<Claim>()
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                            new Claim(ClaimTypes.Email, user.Email),
                            new Claim(ClaimTypes.Name, user.FirstName + user.LastName),

                        };
                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);

                        var properties = new AuthenticationProperties
                        {
                            IsPersistent = loginUserViewModel.RememberMe
                        };
                        TempData[SuccessMessage] = "موفق";
                        await HttpContext.SignInAsync(principal, properties);

                        break;

                    }
                }
            }

            return View(loginUserViewModel);
        }





        #endregion

        #region SuccessRegister

        //[HttpGet()]
        //public IActionResult SuccessRegister()
        //{
        //    ViewBag.email = TempData["Email"];

        //    if (TempData["CheckOtpCode"] == null)
        //    {
        //        return RedirectToAction("Register", "Account");

        //    }
        //    return View();

        //}

        #endregion



        #region ActiveEmailAccount

        [Route("Active")]
 
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult>ActiveEmailAccount(EmailActiveAccountViewModel activeCode)
        {  
            if (ModelState.IsValid)

            {
                var result = await _userService.ActiveAccount(activeCode);
                switch (result)
                {
                    case ActiveEmailResult.Error:
                        ModelState.AddModelError("CustomError", "کاربر عزیز، درخواست شما با خطا مواجه شد. ");
                        //TempData[WarningMessage] = "کاربر عزیز، درخواست شما با خطا مواجه شد";
                        ViewData["AlertClass"] = "alert-danger";
                        break;

                    case ActiveEmailResult.NotActive:
                        ModelState.AddModelError("CustomError", "کاربر عزیز، حساب شما فعال نمی باشد. ");
                        //TempData[ErrorMessage] = "کاربر عزیز، حساب شما فعال نمی باشد";
                        ViewData["AlertClass"] = "alert-warning";
                        break;

                    case ActiveEmailResult.Success:
                        ModelState.AddModelError("CustomError", "کاربر عزیز، حساب شما با موفقیت فعال شد. ");
                        //TempData[SuccessMessage] = "کاربر عزیز، حساب شما با موفقیت فعال شد";
                        ViewData["AlertClass"] = "alert-success";
                        break;

                }

                ViewBag.active = result;
            }


            return View();
        
    }

        #region LogOut

        [Route("LogOut")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            TempData[InfoMessage] = "با موفقیت خارج شدید";
            return RedirectToAction("Login", "Account");
        }

        #endregion


        #endregion


        [Authorize]
         [Route("Test")]
        public IActionResult Test()
        {

            return View();
        }
    }
}
