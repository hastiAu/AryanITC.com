using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AryanITC.Domain.IRepository;
using AryanITC.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AryanITC.Web.ViewComponents
{

             
    public class MainSliderViewComponent : ViewComponent
    {

        //#region Constructor

        //private readonly ISiteService _siteService;

        //public MainSliderViewComponent(ISiteService siteService)
        //{
        //    _siteService = siteService;
        //}

        //#endregion

        public  async Task<IViewComponentResult> InvokeAsync()
        {


            return  View("/Views/Shared/Components/MainSlider/MainSlider.cshtml");
        }
    }
}
