using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AryanITC.Domain.Entities.Common;
using Microsoft.AspNetCore.Http;

namespace AryanITC.Domain.ViewModels.ManagementUser
{
    public class EditUserViewModel : BaseEntity
    {

        public long UserId { get; set; }

        [Display(Name = "FirstName")]
        [MaxLength(100, ErrorMessage = "MaxLength")]
        [Required(ErrorMessage = "Required")]
        public string FirstName { get; set; }

        [Display(Name = "LastName")]
        [MaxLength(100, ErrorMessage = "MaxLength")]
        [Required(ErrorMessage = "Required")]
        public string LastName { get; set; }

        [Display(Name = "Mobile")]
        [MaxLength(100, ErrorMessage = "MaxLength")]
        [Required(ErrorMessage = "Required")]
        [RegularExpression("[0]{1}[9]{1}[0-9]{9}", ErrorMessage = "MobileRegularExpression")]
        public string Mobile { get; set; }

        [Display(Name = "Email")]
        [MaxLength(100, ErrorMessage = "MaxLength")]
        [EmailAddress(ErrorMessage = "InvalidFormatEmail")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [MaxLength(100, ErrorMessage = "MaxLength")]
        [Required(ErrorMessage = "Required")]
        [MinLength(6, ErrorMessage = "NoLessThan6")]
        public string Password { get; set; }

        [Display(Name = "Image")]
        public string ImageAvatar { get; set; }

        [Display(Name = "Image")]
        public IFormFile UserAvatar { get; set; }

        [Display(Name = "IsSuperAdmin")]
        public bool IsSuperAdmin { get; set; }

        [Display(Name = "UserRoles")]
        public List<long> UserRoles { get; set; }

    }

    public enum EditUserTypeResult
    {
        Success,
        EmailExit,
        MobileExit,
        NotFound
    }
}
