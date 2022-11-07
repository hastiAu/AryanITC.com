using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AryanITC.Domain.ViewModels.AboutUs
{
  public class AboutUsViewModel
    {
        public long AboutUsId { get; set; }
        public string AboutUsTitle { get; set; }

        public string AboutUsDescription { get; set; }

        public string AboutUsLink { get; set; }

        public string AboutUsImage { get; set; }

     
    }
}
