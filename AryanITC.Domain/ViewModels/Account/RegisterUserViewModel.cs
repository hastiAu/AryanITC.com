using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AryanITC.Domain.ViewModels.Account
{

    #region Properties

    public class RegisterUserViewModel
    {
        [Display(Name = "Mobile")]
        [MaxLength(100, ErrorMessage = "MaxLength")]
        [Required(ErrorMessage = "Required")]
        [RegularExpression("[0]{1}[9]{1}[0-9]{9}", ErrorMessage = "MobileRegularExpression")]
        public string Mobile { get; set; }


        [Display(Name = "FirstName")]
        [MaxLength(100, ErrorMessage = "MaxLength")]
        [Required(ErrorMessage = "Required")] 
        public string FirstName { get; set; }


        [Display(Name = "LastName")]
        [MaxLength(100, ErrorMessage = "MaxLength")]
        [Required(ErrorMessage = "Required")]
        public string LastName { get; set; }

        [Display(Name = "Password")]
        [MaxLength(100, ErrorMessage = "MaxLength")]
        [Required(ErrorMessage = "Required")]
        [MinLength(6, ErrorMessage = "PasswordRegularExpertion")]
        public string Password { get; set; }


        [Display(Name = "ConfirmPassword")]
        [MaxLength(100, ErrorMessage = "MaxLength")]
        [Required(ErrorMessage = "Required")]
        [Compare("Password", ErrorMessage = "Compare")]
        [MinLength(6, ErrorMessage = "PasswordRegularExpertion")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Email")]
        [MaxLength(50, ErrorMessage = "MaxLength")]
        [Required(ErrorMessage = "Required")]
        [EmailAddress(ErrorMessage = "EmailAddress")]
        public string Email { get; set; }
    }

    #endregion

    #region RegisterUserResult

    public enum RegisterUserResult
    {
        Success,
        UserExist, 
        Error

    }

    #endregion

}
