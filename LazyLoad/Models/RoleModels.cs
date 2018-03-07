using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LazyLoad.Models
{
    public class UserRole
    {
        public Guid ID { get; set; }
        public String Name { get; set; }
    }
    public class UserRoleGruop
    {
        public Guid ID { get; set; }
        public String Name { get; set; }
        public virtual ICollection<RoleJoinRoleGruop> RoleJoinRoleGruops { get; set; }
    }
    public class RoleJoinRoleGruop
    {
        [Key]
        [Column(Order = 1)]
        public Guid UserRoleID { get; set; }
        public virtual UserRole UserRole { get; set; }
        [Key]
        [Column(Order = 2)]
        public Guid UserRoleGruopID { get; set; }
        public virtual UserRoleGruop UserRoleGruop { get; set; }
    }
    public class RoleGruopJoinUsers
    {
        [Key]
        [Column(Order = 2)]
        public Guid UserID { get; set; }
        public virtual ApplicationUser User { get; set; }
        [Key]
        [Column(Order = 2)]
        public Guid UserRoleGruopID { get; set; }
        public virtual UserRoleGruop UserRoleGruop { get; set; }

    }
}