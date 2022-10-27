using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AryanITC.Domain.Entities.AboutUs;
using AryanITC.Domain.IRepository;
using AryanITC.Domain.ViewModels.AboutUs;
using AryanITC.Domain.ViewModels.Pagination;
using AryanITC.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AryanITC.Infra.Data.Repository
{
   public class AboutUsRepository: IAboutUsRepository
    {
        #region Constructor

        private readonly AryanDbContext _context;

        public AboutUsRepository(AryanDbContext context)
        {
            _context = context; 
        }

        #endregion

        #region Save

        public async Task SaveChange()
        {
           await _context.SaveChangesAsync();
        }
        #endregion

        #region AboutUs


        public async Task<FilterAboutUsViewModel> FilterAdminAboutUs(FilterAboutUsViewModel filterAboutUsViewModel)
        {
            IQueryable<AboutUs> query = _context.AboutUs.AsQueryable();
            //var query = _context.AboutUs.AsQueryable();

            #region Filter

            switch (filterAboutUsViewModel.AboutUsState)
            {
                case AboutUsState.All:
                    break;

                case AboutUsState.Deleted:

                    {
                        query = query.Where(u => u.IsDelete);
                        break;
                    }

                case AboutUsState.NotDeleted:

                    {
                        query = query.Where(u => !u.IsDelete);
                        break;
                    }

            }

            if (!string.IsNullOrEmpty(filterAboutUsViewModel.AboutUsTitle))
            {
                query = query.Where(u => u.AboutUsTitle.ToLower().Contains(filterAboutUsViewModel.AboutUsTitle));
            }

            #endregion
            int allEntitiesCount = await query.CountAsync();
            var pager = Pagination.BuildPagination(filterAboutUsViewModel.PageId, allEntitiesCount);
            var about = await query.OrderBy(o => o.IsDelete).Pagination(pager).ToListAsync();
            filterAboutUsViewModel.SetAboutUs(about);
            return filterAboutUsViewModel.SetPaging(pager);

        }

        public async Task CreateAboutUs(AboutUs aboutUs)
        {
            await _context.AboutUs.AddAsync(aboutUs);
        }

        #endregion

    }
}
