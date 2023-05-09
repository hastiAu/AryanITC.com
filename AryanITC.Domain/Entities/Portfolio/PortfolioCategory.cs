﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AryanITC.Domain.Entities.Common;

namespace AryanITC.Domain.Entities.Portfolio
{
    public class PortfolioCategory : BaseEntity
    {
        public long? ParentId { get; set; }

        [Display(Name = "PortfolioTitle")]
        [MaxLength(200, ErrorMessage = "MaxLength")]
        [Required(ErrorMessage = "Required")]
        public string PortfolioTitle { get; set; }

        [Display(Name = "NameInUrl")]
        [MaxLength(200, ErrorMessage = "MaxLength")]
        public string NameInUrl { get; set; }

        [Display(Name = "Order")]
        public int Order { get; set; }

        [Display(Name = "IsActive")]
        public bool IsActive { get; set; }

        [Display(Name = "IsDelete")]
        public bool IsDelete { get; set; }

        #region Relation
        public ICollection<PortfolioSelectedCategory> PortfolioSelectedCategories { get; set; }
        #endregion
    }


}
