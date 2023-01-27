using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AryanITC.Domain.ViewModels.AboutUs;
using AryanITC.Domain.ViewModels.Pagination;
using Microsoft.AspNetCore.Http;

namespace AryanITC.Domain.ViewModels.Service
{
    public class FilterServiceViewModel:BasePagination
    {
        [Display(Name = "ServiceTitle")]
        [Required(ErrorMessage = "Required")]
        [MaxLength(250, ErrorMessage = "MaxLength")]
        public string ServiceTitle { get; set; }

        [Display(Name = "ServiceDescription")]
        [Required(ErrorMessage = "Required")]
        [DataType(DataType.MultilineText)]
        public string ServiceDescription { get; set; }

        [Display(Name = "IsDelete")]
        public bool IsDelete { get; set; }

        [Display(Name = "IsActive")]
        public bool IsActive { get; set; }

        [Display(Name = "ServiceImage")]
 
        public IFormFile ServiceImageFile { get; set; }

        [Display(Name = "FontAwesome")]
        public string FontAwesome { get; set; }

        [Display(Name = "ServiceImage")]
        [MaxLength(600, ErrorMessage = "MaxLength")]
        public string ServiceImage { get; set; }

        [Display(Name = "ServiceState")]
        public ServiceState ServiceState { get; set; }

        public List<Entities.Service.Service> Service { get; set; }
         

        public FilterServiceViewModel SetPaging(BasePagination basePagination)
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
        public FilterServiceViewModel SetService(List<Entities.Service.Service> service)
        {
            Service = service;
            return this;
        }
    }

    public enum ServiceState
    {
        All,
        Deleted,
        NotDeleted
    }

}

