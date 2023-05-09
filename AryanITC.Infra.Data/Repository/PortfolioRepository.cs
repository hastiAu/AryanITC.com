using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AryanITC.Domain.Entities.Portfolio;
using AryanITC.Domain.IRepository;
using AryanITC.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AryanITC.Infra.Data.Repository
{
   public class PortfolioRepository: IPortfolioRepository
    {

        #region Constructor

        private readonly AryanDbContext _context;

        public PortfolioRepository(AryanDbContext context)
        {
            _context = context;
        }

        #endregion
        public async Task SaveChange()
        {
            await _context.SaveChangesAsync();
        }

        public async Task CreatePortfolioCategory(PortfolioCategory portfolioCategory)
        {
              await _context.AddAsync(portfolioCategory);
        }

        public async Task<bool> IsExistPortfolioCategory(long portfolioCategoryId)
        {
            return await _context.PortfolioCategories.AnyAsync(u => u.Id == portfolioCategoryId && u.ParentId == null);

        }
    }
}
