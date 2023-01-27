using AryanITC.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace AryanITC.Domain.Entities.AboutUs
{
    public class AboutUs : BaseEntity
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
        [Required(ErrorMessage = "Required")]
        [MaxLength(350, ErrorMessage = "MaxLength")]
        [Url(ErrorMessage = "Url")]
        public string AboutUsLink { get; set; }

        [Display(Name = "AboutUsImage")]
        [MaxLength(600,ErrorMessage = "MaxLength")]
        public string AboutUsImage { get; set; }
    }
}
