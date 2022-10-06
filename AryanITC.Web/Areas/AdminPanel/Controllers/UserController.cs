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

        public UserController(IUserService userService)
        {
            _userService = userService; 
        }

        #endregion

        #region Index

        public async Task<IActionResult> Index(FilterUserViewModel filter)
        {
            var user = await _userService.FilterUsers(filter);

            return View(user);
        }

        #endregion

    }
}
