namespace LazyLoad.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createDataBase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomersID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Customers", t => t.CustomersID, cascadeDelete: true)
                .Index(t => t.CustomersID);
            
            CreateTable(
                "dbo.InviceDetails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        InvoicesID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Invoices", t => t.InvoicesID, cascadeDelete: true)
                .Index(t => t.InvoicesID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        DepartmentName = c.String(),
                        Description = c.String(),
                        Employees_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employees", t => t.Employees_ID)
                .Index(t => t.Employees_ID);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Position = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RoleGruopJoinUsers",
                c => new
                    {
                        UserID = c.String(nullable: false, maxLength: 128),
                        UserRoleGruopID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserID, t.UserRoleGruopID })
                .ForeignKey("dbo.AspNetUsers", t => t.UserID, cascadeDelete: true)
                .ForeignKey("dbo.UserRoleGruops", t => t.UserRoleGruopID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.UserRoleGruopID);
            
            CreateTable(
                "dbo.UserRoleGruops",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RoleJoinRoleGruops",
                c => new
                    {
                        UserRoleID = c.Guid(nullable: false),
                        UserRoleGruopID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserRoleID, t.UserRoleGruopID })
                .ForeignKey("dbo.UserRoles", t => t.UserRoleID, cascadeDelete: true)
                .ForeignKey("dbo.UserRoleGruops", t => t.UserRoleGruopID, cascadeDelete: true)
                .Index(t => t.UserRoleID)
                .Index(t => t.UserRoleGruopID);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.RoleGruopJoinUsers", "UserRoleGruopID", "dbo.UserRoleGruops");
            DropForeignKey("dbo.RoleJoinRoleGruops", "UserRoleGruopID", "dbo.UserRoleGruops");
            DropForeignKey("dbo.RoleJoinRoleGruops", "UserRoleID", "dbo.UserRoles");
            DropForeignKey("dbo.RoleGruopJoinUsers", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Departments", "Employees_ID", "dbo.Employees");
            DropForeignKey("dbo.Customers", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.InviceDetails", "InvoicesID", "dbo.Invoices");
            DropForeignKey("dbo.Invoices", "CustomersID", "dbo.Customers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.RoleJoinRoleGruops", new[] { "UserRoleGruopID" });
            DropIndex("dbo.RoleJoinRoleGruops", new[] { "UserRoleID" });
            DropIndex("dbo.RoleGruopJoinUsers", new[] { "UserRoleGruopID" });
            DropIndex("dbo.RoleGruopJoinUsers", new[] { "UserID" });
            DropIndex("dbo.Departments", new[] { "Employees_ID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.InviceDetails", new[] { "InvoicesID" });
            DropIndex("dbo.Invoices", new[] { "CustomersID" });
            DropIndex("dbo.Customers", new[] { "UserID" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.UserRoles");
            DropTable("dbo.RoleJoinRoleGruops");
            DropTable("dbo.UserRoleGruops");
            DropTable("dbo.RoleGruopJoinUsers");
            DropTable("dbo.Employees");
            DropTable("dbo.Departments");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.InviceDetails");
            DropTable("dbo.Invoices");
            DropTable("dbo.Customers");
        }
    }
}
