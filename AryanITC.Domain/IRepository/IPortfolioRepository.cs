using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AryanITC.Domain.Entities.Portfolio;

namespace AryanITC.Domain.IRepository
{
   public interface IPortfolioRepository
    {

        #region SaveChanges

        Task SaveChange();

        #endregion

        Task CreatePortfolioCategory(PortfolioCategory portfolioCategory);
        Task<bool> IsExistPortfolioCategory(long portfolioCategoryId);
    }
}
