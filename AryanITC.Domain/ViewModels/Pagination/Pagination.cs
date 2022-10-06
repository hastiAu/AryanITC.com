using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AryanITC.Domain.ViewModels.Pagination
{
    public class Pagination
    {
        public static BasePagination BuildPagination(int pageId, int allEntitiesCount, int take = 15, int howManyAfterBefore = 9)
        {
            int pageCount = (int)Math.Ceiling((double)allEntitiesCount / take);
            return new BasePagination()
            {
                PageId = pageId,
                TakeEntity = take,
                SkipEntity = (pageId - 1) * take,
                AllEntitiesCount = allEntitiesCount,
                AllPageCount = pageCount,
                StartPage = pageId - howManyAfterBefore > 0 ? pageId - howManyAfterBefore : 1,
                EndPage = pageId + howManyAfterBefore > pageCount ? pageCount : pageId + howManyAfterBefore
            };
        }
    }
}
