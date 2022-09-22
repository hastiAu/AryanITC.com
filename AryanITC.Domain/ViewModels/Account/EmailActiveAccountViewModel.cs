using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AryanITC.Domain.ViewModels.Account
{
    #region ActiveEmailAccount
    public class EmailActiveAccountViewModel
    {
 

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string EmailActiveCode { get; set; }

    }
    #endregion

    #region ActiveEmailResult
    public enum ActiveEmailResult
    {
 
        Success,
        NotActive,
        Error,
    }

    #endregion


}
