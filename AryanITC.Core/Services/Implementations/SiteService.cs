using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AryanITC.Core.Services.Interfaces;
using AryanITC.Domain.IRepository;

namespace AryanITC.Core.Services.Implementations
{
    public class SiteService : ISiteService

    {

        #region Costructor

        private readonly ISliderRepository _sliderRepository;

        public SiteService(ISliderRepository sliderRepository)
        {
            _sliderRepository = sliderRepository;
        }

        #endregion

    }
}
