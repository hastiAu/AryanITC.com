using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AryanITC.Domain.Entities.Account;
using AryanITC.Domain.Entities.Common;

namespace AryanITC.Domain.Entities.Access
{
   public class UserRole:BaseEntity
    {
        #region Properties
        public long UserId { get; set; }
        public long RoleId { get; set; }

        #endregion

        #region Relations

        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("RoleId")]
        public Role Role { get; set; }

        #endregion
    }
}
