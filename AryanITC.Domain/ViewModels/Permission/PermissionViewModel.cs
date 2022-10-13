using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AryanITC.Domain.ViewModels.Permission
{
    public class PermissionViewModel
    {
        [Display(Name = "PermissionTitle")]
        public string PermissionTitle { get; set; }

        public long PermissionId { get; set; }

        public long? ParentId { get; set; }
    }
}
