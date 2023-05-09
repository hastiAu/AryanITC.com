using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AryanITC.Domain.Entities.Common;

namespace AryanITC.Domain.Entities.Portfolio
{
    public class PortfolioSelectedCategory: BaseEntity
    {
        [Required]
        public long PortfolioId { get; set; }
        [Required]
        public long PortfolioCategoryId { get; set; }


        #region Relation

        [ForeignKey("PortfolioId")]
        public Portfolio Portfolio { get; set; }
        [ForeignKey("PortfolioCategoryId")]
        public PortfolioCategory PortfolioCategory { get; set; }

        #endregion
    }
}
