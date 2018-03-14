﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BLL.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //public ApplicationDbContext(bool lazyload=true)
        //    : base("DefaultConnection", throwIfV1Schema: false)
        //{
        //    if(!lazyload)
        //    this.Configuration.LazyLoadingEnabled = false;
        //}


        public ApplicationDbContext()
           : base("DefaultConnection", throwIfV1Schema: false)
        { }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<Models.Customers> Customers { get; set; }

        public System.Data.Entity.DbSet<Models.Invoices> Invoices { get; set; }

        public System.Data.Entity.DbSet<Models.InviceDetails> InviceDetails { get; set; }

        public System.Data.Entity.DbSet<Models.Employees> Employees { get; set; }

        public System.Data.Entity.DbSet<Models.Departments> Departments { get; set; }

        public System.Data.Entity.DbSet<Models.UserRole> UserRoles { get; set; }

        public System.Data.Entity.DbSet<Models.UserRoleGruop> UserRoleGruops { get; set; }

        public System.Data.Entity.DbSet<Models.RoleJoinRoleGruop> RoleJoinRoleGruops { get; set; }
        public System.Data.Entity.DbSet<Models.RoleGruopJoinUsers> RoleGruopJoinUsers { get; set; }
        public DbSet<Logger> Loggers { get; set; }
    }
}