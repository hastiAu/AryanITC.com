using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AryanITC.Core.Services.Interfaces;
using AryanITC.Domain.ViewModels.Role;
using AryanITC.Web.Areas.AdminPanel.Controllers;

namespace AryanITC.Web.Areas.AdminPanel.Controllers
{

    public class RoleController : BaseAdminController
    {
        #region Constructor

        private readonly IAccessService _accessService;

        public RoleController(IAccessService accessService)
        {
            _accessService = accessService;
        }

        #endregion


     
        [HttpGet]
        public async Task<IActionResult> Index(FilterRoleViewModel filterRole)
        {
            if (ModelState.IsValid)
            {
                  await _accessService.FilterRoles(filterRole);
            }

            return View(filterRole);
        }

        public async Task GetPermissions()
        {
            var permissions = await _accessService.GetAllPermissions();
            ViewData["permissions"] = permissions;
        }

        [HttpGet]
        public async Task<IActionResult> CreateRole()
        {
            await GetPermissions();
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel createRoleViewModel)
        {
            if (!ModelState.IsValid)
            {

                await GetPermissions();
                return View(createRoleViewModel);
            }

            var res = await _accessService.CreateRole(createRoleViewModel);
            switch (res)
            {
                case RoleTypeResult.Exists:
                    ModelState.AddModelError("RoleTitle", "این نقش وجود دارد");
                    await GetPermissions();
                    return View(createRoleViewModel);

                case RoleTypeResult.NotFound:
                    return RedirectToAction("NotFound");

                case RoleTypeResult.Success:
                    return RedirectToAction("Index");
                   
            }

            return View(createRoleViewModel);
        }
    }
}
