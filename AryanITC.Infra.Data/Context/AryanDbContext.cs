using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using AryanITC.Domain.Entities.AboutUs;
using AryanITC.Domain.Entities.Access;
using AryanITC.Domain.Entities.Account;
using AryanITC.Domain.Entities.Service;
using AryanITC.Domain.Entities.SiteSetting;
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
        public DbSet<SiteSetting> SiteSettings { get; set; }

        #endregion

        #region About US

        public DbSet<AboutUs>AboutUs { get; set; }

        #endregion

        #region Service

        public DbSet<Service>Services { get; set; }

        #endregion
    }
}
