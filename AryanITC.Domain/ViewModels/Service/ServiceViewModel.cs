using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AryanITC.Domain.ViewModels.Service
{
    public class ServiceViewModel
    {
        public string ServiceTitle { get; set; }
        public string ServiceDescription { get; set; }
        public bool IsDelete { get; set; }
        public bool IsActive { get; set; }
        public string ServiceImage { get; set; }
        public string ServiceLink { get; set; }
        public string FontAwesome { get; set; }
        public string FontAwesomeColor { get; set; }
    }
}