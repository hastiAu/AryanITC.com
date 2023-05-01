using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using AryanITC.Domain.Entities.Service;
using AryanITC.Domain.IRepository;
using AryanITC.Domain.ViewModels.Pagination;
using AryanITC.Domain.ViewModels.Service;
using AryanITC.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AryanITC.Infra.Data.Repository
{
    public class ServiceRepository : IServiceRepository
    {
        #region Constructor

        private readonly AryanDbContext _context;

        public ServiceRepository(AryanDbContext context)
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

        #region Service

        public async Task<FilterServiceViewModel> FilterAdminService(FilterServiceViewModel filterServiceViewModel)
        {

            IQueryable<Service> query = _context.Services.AsQueryable();



            switch (filterServiceViewModel.ServiceState)
            {
                case ServiceState.All:
                    break;

                case ServiceState.Deleted:

                {
                    query = query.Where(u => u.IsDelete);
                    break;
                }

                case ServiceState.NotDeleted:

                {
                    query = query.Where(u => !u.IsDelete);
                    break;
                }

            }

            if (!string.IsNullOrEmpty(filterServiceViewModel.ServiceTitle))
            {

                query = query.Where(u => u.ServiceTitle.Contains(u.ServiceTitle.ToLower()));
            }



            int allEntitiesCount = await query.CountAsync();
            var pager = Pagination.BuildPagination(filterServiceViewModel.PageId, allEntitiesCount);
            var service = await query.OrderBy(o => o.IsDelete).Pagination(pager).ToListAsync();
            filterServiceViewModel.SetService(service);
            return filterServiceViewModel.SetPaging(pager);

        }

        public async Task CreateService(Service service)
        {
            await _context.AddAsync(service);
        }

        public async Task<List<ServiceViewModel>> GetAllServiceForShowInSite()
        {
            var service= await _context.Services
                .Where(u => u.IsActive && !u.IsDelete)
                .Select(n => new ServiceViewModel()
                    {
                        ServiceTitle = n.ServiceTitle,
                        ServiceDescription = n.ServiceDescription,
                        FontAwesome = n.FontAwesome,
                        FontAwesomeColor = n.FontAwesomeColor,
                        ServiceImage = n.ServiceImage,
                        ServiceLink = n.ServiceLink
                    }
                ).ToListAsync();

            return service;

            #endregion



        }

    }

}