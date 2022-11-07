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

        public async Task<EditAboutUsViewModel> GetEditAboutUsForEdit(long aboutUsId)
        {
            if (aboutUsId != 0)
            {
                var result = await _context.AboutUs
                    .Where(i => i.Id == aboutUsId)
                    .Select(s => new EditAboutUsViewModel()
                    {
                        AboutUsTitle = s.AboutUsTitle,
                        AboutUsDescription = s.AboutUsDescription,
                        AboutUsLink = s.AboutUsLink,
                        IsActive = s.IsActive,
                        IsDelete = s.IsDelete,
                        AboutUsImage = s.AboutUsImage,
                        AboutUsId = s.Id,
                      
                        // Should Tell Id because knows which id must be edited
                        
                    })

                    .SingleOrDefaultAsync();

                return result;
            }

            return null;
        }

        public void UpdateAboutUs(AboutUs aboutUs)
        {
            _context.AboutUs.Update(aboutUs);
        }

        public async Task<AboutUs> GetAboutUsById(long aboutUsId)
        {
            var aboutUs = await _context.AboutUs.SingleOrDefaultAsync(i => i.Id == aboutUsId);
            return aboutUs;
        }

        public async Task<List<AboutUsViewModel>> GetAllAboutUsForShowInSite()
        {
            return await _context.AboutUs
                .Where(i => !i.IsDelete && i.IsActive)
                .Select(n => new AboutUsViewModel()

                {
                    AboutUsImage = n.AboutUsImage,
                    AboutUsTitle = n.AboutUsTitle,
                    AboutUsDescription = n.AboutUsDescription,
                    AboutUsLink = n.AboutUsLink,
                    AboutUsId = n.Id

                }).ToListAsync();
        }

        #endregion

    }
}
