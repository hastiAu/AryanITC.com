using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AryanITC.Domain.ViewModels.Account
{
    public class SuccessRegisterViewModel
    {


        [Display(Name = "Email")]
        [MaxLength(50, ErrorMessage = "MaxLength")]
        [Required(ErrorMessage = "Required")]
        [EmailAddress(ErrorMessage = "EmailAddress")]
        public string Email { get; set; }
    }
}
