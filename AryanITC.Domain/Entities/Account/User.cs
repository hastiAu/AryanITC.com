using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AryanITC.Domain.Entities.Access;
using AryanITC.Domain.Entities.Common;

namespace AryanITC.Domain.Entities.Account
{
    public class User : BaseEntity
    {
        #region Properties

        [Display(Name = "Mobile")]
        [MaxLength(100, ErrorMessage = "MaxLength")]
        [Required(ErrorMessage = "Required")]
        public string Mobile { get; set; }

        [Display(Name = "FirstName")]
        [MaxLength(100, ErrorMessage = "MaxLength")]
        [Required(ErrorMessage = "Required")]
        public string FirstName { get; set; }

        [Display(Name = "LastName")]
        [MaxLength(100, ErrorMessage = "MaxLength")]
        [Required(ErrorMessage = "")]
        public string LastName { get; set; }

        [Display(Name = "Password")]
        [MaxLength(100, ErrorMessage = "MaxLength")]
        [Required(ErrorMessage = "Required")]
        public string Password { get; set; }

        [Display(Name = "Email")]
        [MaxLength(100, ErrorMessage = "MaxLength")]
        [EmailAddress(ErrorMessage = "EmailAddress")]
        public string Email { get; set; }

        [Display(Name = "EmailActiveCode")]
        [MaxLength(100, ErrorMessage = "MaxLength")]
        public string EmailActiveCode { get; set; }


        [Display(Name = "OtpCode")] 
        public string OtpCode { get; set; }


        [Display(Name = "OtpExpireTime")] 
        public DateTime OtpExpireTime { get; set; }


        [Display(Name = "UserState")] 
        public UserState UserState { get; set; }


        [Display(Name = "RegisterDate")] 
        public DateTime RegisterDate { get; set; }


        [Display(Name = "UserAvatar")]
        [MaxLength(200, ErrorMessage = "MaxLength")]
        public string UserAvatar { get; set; }

        //[Display(Name = "IsActive")]
        //public bool IsActive  { get; set; }

        [Display(Name = "IsDelete")]
        public bool IsDelete { get; set; }

        #endregion

        #region Relations

        public ICollection<UserRole> UserRoles { get; set; }

        #endregion

    }

    public enum UserState
        {

            [Display(Name = "Active")]
            Active,

            [Display(Name = "NotActive")]
            NotActive,

            [Display(Name = "Ban")]
            Ban,

        }

    }
 
