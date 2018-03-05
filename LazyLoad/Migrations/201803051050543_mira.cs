namespace LazyLoad.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mira : DbMigration
    {
        public override void Up()
        {
            
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
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
            
          
            
    
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Departments", "Employees_ID", "dbo.Employees");
            DropForeignKey("dbo.InviceDetails", "InvoicesID", "dbo.Invoices");
            DropForeignKey("dbo.Invoices", "CustomersID", "dbo.Customers");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Departments", new[] { "Employees_ID" });
            DropIndex("dbo.InviceDetails", new[] { "InvoicesID" });
            DropIndex("dbo.Invoices", new[] { "CustomersID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Employees");
            DropTable("dbo.Departments");
            DropTable("dbo.InviceDetails");
            DropTable("dbo.Invoices");
            DropTable("dbo.Customers");
        }
    }
}
