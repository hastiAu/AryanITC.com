using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AryanITC.Web.ViewComponents
{
    public class NewsLetterViewComponent: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {

            return View("/Views/Shared/Components/NewsLetter/NewsLetter.cshtml");
        }
    }
}
