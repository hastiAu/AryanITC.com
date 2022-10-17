using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AryanITC.Domain.ViewModels.Role
{
    public class EditRoleViewModel
    {

        public long RoleId { get; set; }

            [Display(Name = "RoleTitle")]
            [Required(ErrorMessage = "Required")]
            [MaxLength(150, ErrorMessage = "MaxLength")]
            public string RoleTitle { get; set; }

            [Display(Name = "RolePermissions")]
            [Required(ErrorMessage = "Required")]
            public List<long> RolePermissions { get; set; }
        }

        public enum EditRoleTypeResult
        {
            Success,
            Exists,
            NotFound
        }
    }