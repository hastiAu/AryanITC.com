using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AryanITC.Domain.Entities.Common;

namespace AryanITC.Domain.Entities.Access
{
    public class Permission: BaseEntity
    {
        [Display(Name = "PermissionName")]
        [Required]
        [MaxLength(150)]
        public string PermissionName { get; set; }


        [Display(Name = "PermissionTitle")]
        [Required]
        [MaxLength(150)]
        public string PermissionTitle { get; set; }

        public long? ParentId { get; set; }
        //if was Null : Is Parent

        #region Relations

        [ForeignKey("ParentId")]
        public Permission Parent { get; set; }

        public ICollection<RolePermission> RolePermissions { get; set; }
      
        #endregion

    }
}
