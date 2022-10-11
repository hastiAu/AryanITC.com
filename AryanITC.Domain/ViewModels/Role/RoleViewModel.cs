using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AryanITC.Domain.ViewModels.Role
{
   public class RoleViewModel
    {
        public long  RoleId { get; set; }

        [Display(Name = "RoleTitle")]

        public string RoleTitle { get; set; }
    }
}
