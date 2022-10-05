using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AryanITC.Domain.ViewModels.Account
{
    #region ResetPassword

    public class ResetPasswordViewModel
    {
        [Display(Name = "EmailActiveCode")]
        [MaxLength(100, ErrorMessage = "MaxLength")]
        public string EmailActiveCode { get; set; }

        [Display(Name = "Password")]
        [MaxLength(100, ErrorMessage = "MaxLength")]
        [Required(ErrorMessage = "Required")]
        [MinLength(6, ErrorMessage = "PasswordRegularExpression")]
        public string Password { get; set; }


         [Display(Name = "Password")]
         [MaxLength(100, ErrorMessage = "MaxLength")]
         [Required(ErrorMessage = "Required")]
         [MinLength(6,ErrorMessage = "PasswordRegularExpression")]
        public string ConfirmPassword { get; set; }
    }

    #endregion

    #region ResetPasswordResult

    public enum ResetPasswordResult
    {
        Success,
        Error,
        NotValid
    }

    #endregion
}
