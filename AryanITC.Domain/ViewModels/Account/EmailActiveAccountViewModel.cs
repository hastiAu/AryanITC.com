using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AryanITC.Domain.ViewModels.Account
{
   public class EmailActiveAccountViewModel
    {
        #region ActiveEmail

        [Display(Name = "EmailActiveCode")]
        [Required(ErrorMessage = "Required")]
        [MaxLength(200, ErrorMessage = "MaxLength")]
        public string EmailActiveCode { get; set; }

        #endregion

        #region ActiveEmailResult

        public enum ActiveEmailResult
        {
            error,
            Notfound,
            Success
        }

        #endregion
    }
}
