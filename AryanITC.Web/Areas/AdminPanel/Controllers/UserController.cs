using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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
        public async Task<IActionResult> CreateUser()
        {
            await GetRoles();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserViewModel createUser, List<long> userRoles)
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

        #region Edit User

        [HttpGet]
        public async Task<IActionResult> EditUser(long id)
        {
            if (id <= 0) return NotFound();
            var user = await _userService.GetUserForEditById(id);
            if (user == null) return RedirectToAction("NotFound", "Home");
            await GetRoles();
            return View(user);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(EditUserViewModel editUserViewModel )
        {
            if (ModelState.IsValid)
            {
                var res = await _userService.EditUser(editUserViewModel);
                switch (res)
                {
                    case EditUserTypeResult.EmailExit:
                        ModelState.AddModelError("Email","با این ایمیل قبلا ثبت نام انجام شده است");
                       await GetRoles();
                        return View(editUserViewModel);

                    case EditUserTypeResult.MobileExit:
                        ModelState.AddModelError("Mobile","با این َماره موبایل قبلا ثبت نام انجام شده است");
                        await GetRoles();
                        return View(editUserViewModel);

                    case EditUserTypeResult.NotFound:
                        return RedirectToAction("NotFound");

                    case EditUserTypeResult.Success:
                        TempData[SuccessMessage] = " ویرایش با موفقیت انحام شد";
                        return RedirectToAction("Index");

                }

            }
            return View(editUserViewModel);

        }
        #endregion

        #region Delete User

        [HttpPost]
        public async Task<IActionResult> DeleteUser(long id)
        {
            var result = await _userService.DeleteUser(id);
            switch (result)
            {
                case DeleteUserResult.UserNotFound:
                    return Json(new
                    {
                        text = "کاربر مورد نظر یافت نشد"
                    });


                case DeleteUserResult.SuccessDeleted:
                    return Json(new
                    {
                        text = " کاربر با موفقیت حذف شد",
                        statusCode = HttpStatusCode.OK
                        //for Page Refresh after Delete
                    });

            }

            return RedirectToAction("Index");

        }

        #endregion

        #region Restore User

        [HttpPost]
        public async Task<IActionResult> RestoreUser(long id)
        {
            var result = await _userService.RestoreUser(id);
 
            switch (result)
            {
                case RestoreUserResult.NotFound:
                    return Json(new
                    {
                        text = "کاربر مورد نظر یافت نشد"
                    });


                case RestoreUserResult.SuccessRestore:
                    return Json(new
                    {
                        text = " کاربر با موفقیت بازگردانی شد",
                        statusCode = HttpStatusCode.OK
                        //for Page Refresh after Delete
                    });

            }

            return RedirectToAction("Index");

        }

        #endregion
    }
}
