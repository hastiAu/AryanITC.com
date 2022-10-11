using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AryanITC.Core.Services.Interfaces;
using AryanITC.Domain.ViewModels.ManagementUser;

namespace AryanITC.Web.Areas.AdminPanel.Controllers
{
    public class UserController : BaseAdminController
    {

        #region Constractor

        private readonly IUserService _userService;
        private readonly IAccessService _accessService;

        public UserController(IUserService userService, IAccessService accessService)
        {
            _userService = userService;
            _accessService = accessService;
        }

        #endregion

        #region Index

        public async Task<IActionResult> Index(FilterUserViewModel filter)
        {
            var user = await _userService.FilterUsers(filter);

            return View(user);
        }

        #endregion


        #region Get Roles

        public async Task GetRoles()
        {
            var userRoles = await _accessService.GetAllRoles();
            ViewData["Roles"] = userRoles;
        }


        #endregion
        #region Create User
        [HttpGet]
        public async  Task<IActionResult> CreateUser()
        {
            await GetRoles();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserViewModel createUser)
        {
            if (!ModelState.IsValid)
            {
                await GetRoles();
                return View(createUser);
            }

            var result = await _userService.CreateUser(createUser);
            switch (result)
            {
                case UserTypeResult.EmailExit:
                    ModelState.AddModelError("Email", "این ایمیل قبلاً ثبت شده است");
                    await GetRoles();
                    return View(createUser);

                case UserTypeResult.MobileExit:

                    ModelState.AddModelError("Email", "این موبایل قبلاً ثبت شده است");
                    await GetRoles();
                    return View(createUser);

                case UserTypeResult.NotFound:
                    ModelState.AddModelError("Email", "کاربری با مشحصات ارسالی یافت نشد");
                    await GetRoles();
                    return View(createUser);

                case UserTypeResult.Success:
                    ModelState.AddModelError("Email", "کاربر با موفقیت اضافه شد");
                    TempData[SuccessMessage] = "کاربر با موفقیت اضافه شد";
                    return RedirectToAction("Index");


            }
            return View(createUser);
        }

        #endregion

    }
}
