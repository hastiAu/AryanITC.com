﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AryanITC.Domain.ViewModels.Account
{
    #region ForgotPassword
    public class ForgotPasswordViewModel
    {
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Email { get; set; }
    }

    #endregion

    #region ForgotPasswordResult

    public enum ForgotPasswordResult
    {
        Error,
        EmailNotValid,
        Success
    }

    #endregion

}
