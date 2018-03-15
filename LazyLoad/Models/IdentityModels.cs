using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LazyLoad.Models
{
    public class ApplicationDbContext : IdentityDbContext<BLL.Models.User>
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

        public System.Data.Entity.DbSet<BLL.Models.UserRole> UserRoles { get; set; }

        public System.Data.Entity.DbSet<BLL.Models.UserRoleGruop> UserRoleGruops { get; set; }

        public System.Data.Entity.DbSet<BLL.Models.RoleJoinRoleGruop> RoleJoinRoleGruops { get; set; }
        public System.Data.Entity.DbSet<BLL.Models.RoleGruopJoinUsers> RoleGruopJoinUsers { get; set; }
        public DbSet<BLL.LoggerModels.Logger> Loggers { get; set; }
    }
}