using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;




namespace AryanITC.Web.Areas.UserPanel.Controllers
{

    [Area("UserPanel")]
    public class BaseUserPanelController : Controller
    {
        protected string SuccessMessage = "SuccessMessage";
        protected string ErrorMessage = "ErrorMessage";
        protected string WarningMessage = "WarningMessage";
        protected string InfoMessage = "InfoMessage";


        public IActionResult NotFound()
        {
            return View();
        }
    }
}
