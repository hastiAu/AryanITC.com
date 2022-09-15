using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AryanITC.Domain.IRepository;
using AryanITC.Infra.Data.Context;

namespace AryanITC.Infra.Data.Repository
{
    public class SliderRepository:ISliderRepository
    {

        #region Constructor

        private readonly AryanDbContext _context;
        public SliderRepository(AryanDbContext context)
        {
            _context = context;
        }

        #endregion
    }
}
