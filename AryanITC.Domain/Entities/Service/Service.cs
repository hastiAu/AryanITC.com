using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AryanITC.Domain.Entities.Common;

namespace AryanITC.Domain.Entities.Service
{
   public class Service: BaseEntity
    {

        [Display(Name = "ServiceTitle")]
        [Required(ErrorMessage = "Required")]
        [MaxLength(250, ErrorMessage = "MaxLength")]
        public string  ServiceTitle { get; set; }

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
        [MaxLength(350, ErrorMessage = "MaxLength")]
        [Url(ErrorMessage = "Url")]
        public string ServiceLink { get; set; }

        [Display(Name = "ServiceImage")]
        //[Required(ErrorMessage = "Required")]
        [MaxLength(600, ErrorMessage = "MaxLength")]

        public string ServiceImage { get; set; }

        [Display(Name = "FontAwesome")]
        public string FontAwesome { get; set; }

        [Display(Name = "FontAwesomeColor")]
        public string FontAwesomeColor { get; set; }
      


    }
}
