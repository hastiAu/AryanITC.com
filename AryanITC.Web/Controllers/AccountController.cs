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

        public AccountController(IUserService userService, IStringLocalizer<AccountController> localizer)
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
                        ModelState.AddModelError(string.Empty, "شکست");
                        TempData[ErrorMessage] = "شکست";
                        break;

                    case RegisterUserResult.UserExist:
                        ModelState.AddModelError("Email", "بجه قبلا ثبت نام کردید");
                        TempData[WarningMessage] = "بچه جون قبلا ثبت نام کردید";
                        return View(registerUserViewModel);


                    case RegisterUserResult.Success:
                        //ModelState.AddModelError("Email", "ب موفقیت ثبت نام کردید");
                        //TempData["Email"] = registerUserViewModel.Email;
                        //TempData[SuccessMessage] = "با موفقیت ثبت نام کردید";
                        return View("SuccessRegister", registerUserViewModel);




                }

            }

            return RedirectToAction("Index", "Home");
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

        public async Task<IActionResult>ActiveEmailAccount(EmailActiveAccountViewModel active)
        //{
        //    if (ModelState.IsValid)
        {
            var result = await _userService.ActiveAccount(active);
            switch (result)
            {
                case ActiveEmailResult.Error:
                    ModelState.AddModelError("email", "حطا");
                    //TempData[WarningMessage] = "Error";

                    break;

                case ActiveEmailResult.NotActive:
                    ModelState.AddModelError("string", "فعال نیست");
                    TempData[ErrorMessage] = "NotActive";
                    break;

                case ActiveEmailResult.Success:
                    ModelState.AddModelError(string.Empty, "  یوزر فعال شدی  ");
                    TempData[SuccessMessage] = "فعال شدی";
                    break;


            }

            ViewData["active"] = result;




            return View(active);
        
    }

        //خروج را بنویسم


        #endregion


        [Authorize]
        [Route("Test")]
        public IActionResult Test()
        {

            return View();
        }
    }
}
