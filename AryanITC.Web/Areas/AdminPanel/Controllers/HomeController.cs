using AryanITC.Core.Security;
using AryanITC.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;

namespace AryanITC.Web.Areas.AdminPanel.Controllers
{
    public class HomeController : BaseAdminController
    {
       
        [HttpGet("AdminPanel")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
