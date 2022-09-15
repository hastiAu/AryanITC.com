using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AryanITC.Web.ViewComponents
{
    public class PortfolioViewComponent: ViewComponent
    {

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("/Views/Shared/Components/Portfolio/Portfolio.cshtml");
        }

       
    }
}
