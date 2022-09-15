using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace AryanITC.Web.Controllers
{
    public class HomeController : SiteBaseController 
    {
        public IActionResult Index()
        {
            return View();
        }

       
    }
}
