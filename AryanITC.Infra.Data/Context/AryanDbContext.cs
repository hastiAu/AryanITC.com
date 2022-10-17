using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using AryanITC.Domain.Entities.Access;
using AryanITC.Domain.Entities.Account;
using Microsoft.EntityFrameworkCore;

namespace AryanITC.Infra.Data.Context
{
    public class AryanDbContext : DbContext

    {
        public AryanDbContext(DbContextOptions<AryanDbContext> options) : base(options)
        {

        }

        #region User
        public DbSet<User> Users { get; set; }
        #endregion

        #region Access

        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Permission> Permission { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }


        #endregion
    }
}
