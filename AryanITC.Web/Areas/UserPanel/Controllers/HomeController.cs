using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AryanITC.Web.Areas.UserPanel.Controllers
{
    public class HomeController : BaseUserPanelController
    {
        [HttpGet("UserPanel")]

        public IActionResult Index()
        {
            return View();
        }
    }
}
