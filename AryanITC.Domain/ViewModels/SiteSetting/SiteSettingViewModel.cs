using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AryanITC.Domain.ViewModels.SiteSetting
{
   public class SiteSettingViewModel
    {
        public long SiteSettingId { get; set; }

        [Display(Name = "SiteName")]
        public string SiteName { get; set; }

        [Display(Name = "SiteUrl")]
        [Url(ErrorMessage = "Url")]
        public string SiteUrl { get; set; }


        [Display(Name = "AboutUs")]
        [DataType(DataType.MultilineText)]
        public string AboutUs { get; set; }

        [Display(Name = "AryanServiceTitle")]
        public string AryanServiceTitle { get; set; }

        [Display(Name = "AryanServiceDescription")]
        public string AryanServiceDescription { get; set; }

        [Display(Name = "AryanPortfolioTitle")]
        public string AryanPortfolioTitle { get; set; }

        [Display(Name = "AryanPortfolioGroupName")]
        public string AryanPortfolioGroupName { get; set; }


        [Display(Name = "AryanHostPriceTitle")]
        public string AryanHostPriceTitle { get; set; }

        [Display(Name = "AryanHostPriceDescription")]
        public string AryanHostPriceDescription { get; set; }

        [Display(Name = "AryanDomainTitle")]
        public string AryanDomainTitle { get; set; }

        [Display(Name = "AryanDomainDescription")]
        public string AryanDomainDescription { get; set; }

        [Display(Name = "AryanCloudHostTitle")]
        public string AryanCloudHostTitle { get; set; }

        [Display(Name = "AryanCloudHostDescription")]
        public string AryanCloudHostDescription { get; set; }

        [Display(Name = "SitePhone")]
        public string SitePhone { get; set; }

        [Display(Name = "SiteEmailAddress")]
        [EmailAddress(ErrorMessage = "EmailAddress")]
        public string SiteEmail { get; set; }

        [Display(Name = "Address")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Display(Name = "CopyRight")]
        public string CopyRight { get; set; }


        [Display(Name = "SiteLogo")]
        public string SiteLogo { get; set; }

        [Display(Name = "LogoFile")]
        public IFormFile LogoFile { get; set; }

    }

    public enum SiteSettingEditResult
   {
       Success,
       NotFound
   }
}
