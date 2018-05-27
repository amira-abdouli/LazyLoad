using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LazyLoad.Models
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

        public DbSet<Customers> Customers { get; set; }

        public DbSet<Invoices> Invoices { get; set; }

        public DbSet<InviceDetails> InviceDetails { get; set; }

        public DbSet<Employees> Employees { get; set; }

        public DbSet<Departments> Departments { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<UserRoleGruop> UserRoleGruops { get; set; }

        public DbSet<RoleJoinRoleGruop> RoleJoinRoleGruops { get; set; }

        public DbSet<RoleGruopJoinUsers> RoleGruopJoinUsers { get; set; }

    }
}