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


        [Display(Name = "FirstName")]
        [MaxLength(100, ErrorMessage = "MaxLength")]
        [Required(ErrorMessage = "Required")]
        public string FirstName { get; set; }


        [Display(Name = "LastName")]
        [MaxLength(100, ErrorMessage = "MaxLength")]
        [Required(ErrorMessage = "Required")]
        public string LastName { get; set; }

        public string Email { get; set; }
    }
}
