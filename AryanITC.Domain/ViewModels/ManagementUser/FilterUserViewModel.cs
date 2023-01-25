using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AryanITC.Domain.Entities.Account;
using AryanITC.Domain.ViewModels.Pagination;
using Microsoft.AspNetCore.Http;

namespace AryanITC.Domain.ViewModels.ManagementUser
{
    public class FilterUserViewModel:BasePagination
    {

        public string FullName { get; set; }
        public string Email  { get; set; }

        public string MobileNumber { get; set; }
        public DateTime RegisterDate { get; set; }
        public List<User> Users { get; set; }

        [Display(Name = "UsersState")]
        public UserState UserState { get; set; }

        public FilterUserState FilterUserState  { get; set; }
        public string ImageAvatar { get; set; }
        public IFormFile UserAvatar { get; set; }

        public FilterUserViewModel SetPaging(BasePagination basePagination)
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

        public FilterUserViewModel SetUsers(List<User> users)
        {
            Users = users;
            return this;
        }

    }

    public enum FilterUserState
    {
        [Display(Name = "همه")]
        All,
        [Display(Name = "Active")]
        Active,
        [Display(Name = "Deactivate")]
        Deactivate,
        [Display(Name = "Deleted")]
        Deleted,
        [Display(Name = "Banned")]
        Banned,
    }
}
