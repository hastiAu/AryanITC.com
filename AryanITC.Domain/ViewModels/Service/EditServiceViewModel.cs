using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AryanITC.Domain.ViewModels.Service
{
    public class EditServiceViewModel
    {
        public long serviceId { get; set; }

        [Display(Name = "ServiceTitle")]
        [Required(ErrorMessage = "Required")]
        [MaxLength(250, ErrorMessage = "MaxLength")]
        public string ServiceTitle { get; set; }

        [Display(Name = "ServiceDescription")]
        [Required(ErrorMessage = "Required")]
        [DataType(DataType.MultilineText)]
        public string ServiceDescription { get; set; }

        [Display(Name = "IsDelete")]
        public bool IsDelete { get; set; }

        [Display(Name = "IsActive")]
        public bool IsActive { get; set; }

        [Display(Name = "ServiceLink")]
        [Required(ErrorMessage = "Required")]
        [MaxLength(600, ErrorMessage = "MaxLength")]
        [Url(ErrorMessage = "Url")]
        public string ServiceLink { get; set; }

        [Display(Name = "ServiceImage")]
        [Required(ErrorMessage = "Required")]
        public IFormFile ServiceImageFile { get; set; }

        [Display(Name = "FontAwesome")]
        public string FontAwesome { get; set; }

        [Display(Name = "FontAwesomeColor")]
        public string FontAwesomeColor { get; set; }
    }

    public enum EditServiceResult
    {
        Success,
        NotFound,
        Error
    }
}
