using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AryanITC.Domain.ViewModels.Account
{
    public class LoginUserViewModel
    {
        #region Properties

        [Display(Name = "Email")]
        [MaxLength(50, ErrorMessage = "MaxLength")]
        [Required(ErrorMessage = "Required")]
        [EmailAddress(ErrorMessage = "EmailAddress")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [MaxLength(100, ErrorMessage = "MaxLength")]
        [Required(ErrorMessage = "Required")]
        [MinLength(6, ErrorMessage = "PasswordRegularExpertion")]
        public string Password { get; set; }

        [Display(Name = "RememberMe")]
        public bool RememberMe { get; set; }

        //[Display(Name = "IsActive")]
        //public bool IsActive { get; set; }
        #endregion

        #region LoginUserREsult

        public enum LoginUserResult
        {
            Success,
            NotActive,
            UserNotFound,
            Error
        }

        #endregion
    }
}
