using AryanITC.Core.Services.Interfaces;
using AryanITC.Domain.ViewModels.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
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
                        ModelState.AddModelError(string.Empty, "کاربر گرامی،ثبت نام شما با خطا مواجه شد.");

                        TempData[ErrorMessage] = "کاربر گرامی،ثبت نام شما با خطا مواجه شد.";
                        break;

                    case RegisterUserResult.UserExist:
                        ModelState.AddModelError("Email", "کاربر گرامی، شما قبلا با ایمیل ثبت نام کرده اید.");
                        TempData[WarningMessage] = "کاربر گرامی، شما قبلا با ایمیل ثبت نام کرده اید.";
                        return View(registerUserViewModel);


                    case RegisterUserResult.Success:
                        TempData[SuccessMessage] = "کاربر گرامی،ثبت نام شما با موفقیت انجام شد.";


                        var user = await _userService.GetUserByEmail(registerUserViewModel.Email);
                        var activeCode = user.EmailActiveCode;

                        ViewBag.active = activeCode;


                        return View("SuccessRegister", registerUserViewModel);

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
                            ModelState.AddModelError("Email", "کاربر گرامی، درخواست شما با خطا مواجه شد");
                            TempData[ErrorMessage] = "کاربر گرامی، درخواست شما با خطا مواجه شد";
                            break;
                        }

                    case LoginUserResult.NotActive:
                        {
                            ModelState.AddModelError("Email", "کاریر گرامی، حساب کاربری شما فعال نیست");
                            TempData[WarningMessage] = "کاریر گرامی، حساب کاربری شما فعال نیست ";
                            break;
                        }
                    case LoginUserResult.UserNotFound:
                        {
                            ModelState.AddModelError("Email", "کاربر پیدا نشد");
                            TempData[WarningMessage] = "کاربری با مشحصات ارسالی یافت نشد";

                            break;

                        }
                    case LoginUserResult.Success:

                        {
                            ViewBag.IsSuccess = true;


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
                            TempData[SuccessMessage] = "ورود شما با موفقیت انجام شد";
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
        public async Task<IActionResult> ActiveEmailAccount(EmailActiveAccountViewModel activeCode)
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


            return View(activeCode);

        }

        #endregion

        #region LogOut

        [Route("LogOut")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            TempData[InfoMessage] = "کاربر عزیز، شما با موفقیت خارج شدید";
            return RedirectToAction("Login", "Account");
        }

        #endregion

        #region ForgotPassword

        [Route("ForgotPassword")]
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel forget)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.ForgotPassword(forget);
                switch (result)
                {
                    case ForgotPasswordResult.Error:
                        ModelState.AddModelError("Email", "کاریر گرامی ، درخواست شما با خطا مواجه شد");
                        TempData[ErrorMessage] = "کاربر گرامی، درخواست شما با خطا مواجه شد";
                        break;
                    case ForgotPasswordResult.EmailNotValid:
                        ModelState.AddModelError("Email", "کاریر گرامی ،ایمیلی با مشخصات وارد شده یافت نشد");
                        TempData[WarningMessage] = "کاریر گرامی ،ایمیلی با مشخصات وارد شده یافت نشد";
                        break;
                    case ForgotPasswordResult.Success:
                        ModelState.AddModelError("Email", "کاریر گرامی ، ایمیل با موفقیت جهت بازبابی کلمه عبور برای شما ارسال شد");
                        TempData[SuccessMessage] =
                            "کاریر گرامی ، ایمیل با موفقیت جهت بازبابی کلمه عبور برای شما ارسال شد";
                        ViewBag.IsSuccess = true;
                        var user = await _userService.GetUserByEmail(forget.Email);
                        if (user == null)
                            return RedirectToAction("NotFound", "Home");
                        break;
                }

            }

            return View(forget);

        }
        #endregion

        #region Reset Password

        [Route("ResetPassword")]
        [HttpGet]
        public IActionResult ResetPassword(string emailActiveCode)
        {
            var reset = new ResetPasswordViewModel
            {
                EmailActiveCode = emailActiveCode
            };
            return View(reset);
        }

        [Route("ResetPassword")]
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel reset)
        {
            if (ModelState.IsValid)

            {
                var result = await _userService.ResetPassword(reset);

                switch (result)
                {
                    case ResetPasswordResult.NotValid:
                        ModelState.AddModelError("Email", "کاریر گرامی ، درخواست شما با خطا مواجه شد");
                        TempData[WarningMessage] = "کاربر عزیز، درخواست شما با خطا مواجه شد";
                        return View(reset);

                    case ResetPasswordResult.Error:
                        ModelState.AddModelError("Email", "کاریر گرامی ،ایمیلی با مشخصات وارد شده یافت نشد");
                        TempData[ErrorMessage] = "کاریر گرامی ،ایمیلی با مشخصات وارد شده یافت نشد";
                        return View(reset);

                    case ResetPasswordResult.Success:
                        ModelState.AddModelError("Email", "کاریر گرامی ، کلمه عبور شما با موفقیت تغییر کرد. می توانید وارد حساب کاربری خود شوید");
                        TempData[SuccessMessage] =
                            "کاریر گرامی ، کلمه عبور شما با موفقیت تغییر کرد. می توانید وارد حساب کاربری خود شوید";
                        return RedirectToAction("Login", "Account");
                }

            }

            return View(reset);
        }
        #endregion

        [Authorize]
        [Route("Test")]
        public IActionResult Test()
        {

            return View();
        }
    }
}
