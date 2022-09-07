 
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace AryanITC.Web.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    //[Authorize]
    public class BaseAdminController :Controller
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
 
