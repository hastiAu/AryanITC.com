using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AryanITC.Domain.Entities.Common;

namespace AryanITC.Domain.Entities.Portfolio
{
    public class Portfolio: BaseEntity
    {
        public long? UserId { get; set; }

        [Display(Name = "PortfolioTitle")]
        [Required(ErrorMessage = "Required")]
        [MaxLength(150)]
        public string PortfolioTitle { get; set; }

        [Display(Name = "PortfolioImageName")]
        [Required(ErrorMessage = "Required")]
        public string PortfolioImageName { get; set; }

        [Display(Name = "PortfolioDescription")]
        [Required(ErrorMessage = "Required")]
        public string PortfolioDescription { get; set; }

        [Display(Name = "Order")]
        public int Order { get; set; }

        [Display(Name = "CreateDate")]
        [Required(ErrorMessage = "Required")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "IsDeleted")]
        [Required(ErrorMessage = "Required")]
        public bool IsDeleted { get; set; }

        #region Relation

        public ICollection<PortfolioSelectedCategory> PortfolioSelectedCategories { get; set; }

        #endregion

    }
}
