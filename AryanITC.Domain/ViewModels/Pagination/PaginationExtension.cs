using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AryanITC.Domain.ViewModels.Pagination
{
    public static class PaginationExtension
    {
        public static IQueryable<T> Pagination<T>(this IQueryable<T> query, BasePagination basePagination)
        {
            return query.Skip(basePagination.SkipEntity).Take(basePagination.TakeEntity);
        }
    }
}
