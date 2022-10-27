using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AryanITC.Domain.Entities.Common;

namespace AryanITC.Domain.Entities.Access
{
    public class Role:BaseEntity
    {
        #region Properties

        [Display(Name = "RoleTitle")]
        [Required(ErrorMessage = "Required")]
        [MaxLength(200, ErrorMessage = "MaxLength")]
        public string RoleTitle { get; set; }

        [Display(Name = "IsDeleted")]
        public bool IsDelete { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        #endregion


        #region Relations

        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<RolePermission> RolePermissions { get; set; }
        // 1 Role can have N RolePermission
        #endregion



    }
}
