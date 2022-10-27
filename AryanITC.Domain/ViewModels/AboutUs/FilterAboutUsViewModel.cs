using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AryanITC.Domain.ViewModels.Pagination;
using AryanITC.Domain.ViewModels.Role;
using Microsoft.AspNetCore.Http;

namespace AryanITC.Domain.ViewModels.AboutUs
{
    public class FilterAboutUsViewModel: BasePagination
    {
       


        [Display(Name = "AboutUsTitle")]
        [Required(ErrorMessage = "Required")]
        [MaxLength(250, ErrorMessage = "MaxLength")]
        public string AboutUsTitle { get; set; }


        [Display(Name = "AboutUsDescription")]
        [Required(ErrorMessage = "Required")]
        [DataType(DataType.MultilineText)]
        public string AboutUsDescription { get; set; }

        [Display(Name = "IsActive")]
        public bool IsActive { get; set; }

        [Display(Name = "IsDelete")]
        public bool IsDelete { get; set; }

        [Display(Name = "AboutUsLink")]
        [MaxLength(350, ErrorMessage = "MaxLength")]
        [Url(ErrorMessage = "Url")]
        public string AboutUsLink { get; set; }

        [Display(Name = "AboutUsImage")]
        [Required(ErrorMessage = "Required")]
        [MaxLength(600, ErrorMessage = "MaxLength")]
        public string AboutUsImage { get; set; }

        [Display(Name = "Image")]
        public IFormFile FormFile { get; set; }

        [Display(Name = "AboutUsState")]
        public AboutUsState AboutUsState { get; set; }
        public List<Entities.AboutUs.AboutUs> AboutUs { get; set; }

        public FilterAboutUsViewModel SetPaging(BasePagination basePagination)
        {
            PageId = basePagination.PageId;
            TakeEntity = basePagination.TakeEntity;
            SkipEntity = basePagination.SkipEntity;
            AllEntitiesCount = basePagination.AllEntitiesCount;
            AllPageCount = basePagination.AllPageCount;
            StartPage = basePagination.StartPage;
            EndPage = basePagination.EndPage;
            return this;
        }

        public FilterAboutUsViewModel SetAboutUs(List<Entities.AboutUs.AboutUs> aboutUs)
        {
            AboutUs = aboutUs;
            return this;
        }
    }


    public enum AboutUsState
    {
        All,
        Deleted,
        NotDeleted
    }


}
