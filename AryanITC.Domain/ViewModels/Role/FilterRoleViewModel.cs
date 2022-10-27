using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AryanITC.Domain.ViewModels.Pagination;

namespace AryanITC.Domain.ViewModels.Role
{
    public class FilterRoleViewModel:BasePagination
    {

    
        [Display(Name = "RoleTitle")]
        public string RoleTitle { get; set; }
        public List<Entities.Access.Role> Roles { get; set; }

        [Display(Name = "FilterRoleState")]
        public FilterRoleState FilterRoleState { get; set; }

        public FilterRoleViewModel SetPaging(BasePagination basePagination)
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

        public FilterRoleViewModel SetRoles(List<Entities.Access.Role> roles)
        {
            Roles = roles;
            return this;
        }
    }

    public enum FilterRoleState
    {
        All, 
        Deleted, 
        NotDeleted
    }

 
}
