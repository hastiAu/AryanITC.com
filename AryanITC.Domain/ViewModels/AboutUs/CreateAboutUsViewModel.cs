using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AryanITC.Domain.ViewModels.AboutUs
{
    public class CreateAboutUsViewModel
    {
        [Display(Name = "AboutUsTitle")]
        [Required(ErrorMessage = "Required")]
        [MaxLength(250, ErrorMessage = "MaxLength")]
        public string AboutUsTitle { get; set; }


        [Display(Name = "AboutUsDescription")]
        [Required(ErrorMessage = "Required")]
        [DataType(DataType.MultilineText)]
        public string AboutUsDescription { get; set; }

        [Display(Name = "IsActive")]
        public bool IsActive { get; set; }

        [Display(Name = "IsDelete")]
        public bool IsDelete { get; set; }

        [Display(Name = "AboutUsLink")]
        [MaxLength(350, ErrorMessage = "MaxLength")]
        [Url(ErrorMessage = "Url")]
        public string AboutUsLink { get; set; }


        [Display(Name = "Image")]
        [Required(ErrorMessage = "Required")]
        public IFormFile AboutUsImage { get; set; }

    }


    public enum CreateAboutUsResult
    {
        Success,
        NotFound,
        Error
    }
}
