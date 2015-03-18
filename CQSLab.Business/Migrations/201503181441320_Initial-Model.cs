namespace CQSLab.Business.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo._DP_BudgetsChannel",
                c => new
                    {
                        ChannelId = c.Int(nullable: false),
                        AccountantPeriod = c.Int(nullable: false),
                        January = c.Decimal(nullable: false, precision: 18, scale: 2),
                        February = c.Decimal(nullable: false, precision: 18, scale: 2),
                        March = c.Decimal(nullable: false, precision: 18, scale: 2),
                        April = c.Decimal(nullable: false, precision: 18, scale: 2),
                        May = c.Decimal(nullable: false, precision: 18, scale: 2),
                        June = c.Decimal(nullable: false, precision: 18, scale: 2),
                        July = c.Decimal(nullable: false, precision: 18, scale: 2),
                        August = c.Decimal(nullable: false, precision: 18, scale: 2),
                        September = c.Decimal(nullable: false, precision: 18, scale: 2),
                        October = c.Decimal(nullable: false, precision: 18, scale: 2),
                        November = c.Decimal(nullable: false, precision: 18, scale: 2),
                        December = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.ChannelId, t.AccountantPeriod })
                .ForeignKey("dbo._DP_Channels", t => t.ChannelId)
                .Index(t => t.ChannelId);
            
            CreateTable(
                "dbo._DP_Channels",
                c => new
                    {
                        ChannelId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 450),
                    })
                .PrimaryKey(t => t.ChannelId);
            
            CreateTable(
                "dbo._DP_Stores",
                c => new
                    {
                        StoreId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 450),
                        ChannelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StoreId)
                .ForeignKey("dbo._DP_Channels", t => t.ChannelId)
                .Index(t => t.ChannelId);
            
            CreateTable(
                "dbo._DP_BudgetsStore",
                c => new
                    {
                        StoreId = c.Int(nullable: false),
                        AccountantPeriod = c.Int(nullable: false),
                        January = c.Decimal(nullable: false, precision: 18, scale: 2),
                        February = c.Decimal(nullable: false, precision: 18, scale: 2),
                        March = c.Decimal(nullable: false, precision: 18, scale: 2),
                        April = c.Decimal(nullable: false, precision: 18, scale: 2),
                        May = c.Decimal(nullable: false, precision: 18, scale: 2),
                        June = c.Decimal(nullable: false, precision: 18, scale: 2),
                        July = c.Decimal(nullable: false, precision: 18, scale: 2),
                        August = c.Decimal(nullable: false, precision: 18, scale: 2),
                        September = c.Decimal(nullable: false, precision: 18, scale: 2),
                        October = c.Decimal(nullable: false, precision: 18, scale: 2),
                        November = c.Decimal(nullable: false, precision: 18, scale: 2),
                        December = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.StoreId, t.AccountantPeriod })
                .ForeignKey("dbo._DP_Stores", t => t.StoreId)
                .Index(t => t.StoreId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Surname = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.OrderLines",
                c => new
                    {
                        OrderLineId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.OrderLineId)
                .ForeignKey("dbo.Orders", t => t.OrderId)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        IsPaid = c.Boolean(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Description = c.String(maxLength: 450),
                        Category = c.String(),
                        Tag = c.String(),
                        ImageUrl = c.String(),
                    })
                .PrimaryKey(t => t.ProductId);
            
            CreateTable(
                "dbo.IdentityRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserRole",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.IdentityRole", t => t.IdentityRole_Id)
                .ForeignKey("dbo.IdentityUser", t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ProviderName = c.String(),
                        Name = c.String(nullable: false, maxLength: 50),
                        Surname = c.String(),
                        Company = c.String(),
                        BirhDate = c.DateTime(),
                        Address = c.String(),
                        PostalCode = c.String(),
                        City = c.String(),
                        Phone = c.String(),
                        Phone2 = c.String(),
                        Image = c.Binary(),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IdentityUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.IdentityUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRole", "ApplicationUser_Id", "dbo.IdentityUser");
            DropForeignKey("dbo.IdentityUserLogin", "ApplicationUser_Id", "dbo.IdentityUser");
            DropForeignKey("dbo.IdentityUserClaim", "ApplicationUser_Id", "dbo.IdentityUser");
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropForeignKey("dbo.OrderLines", "ProductId", "dbo.Products");
            DropForeignKey("dbo.OrderLines", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo._DP_Stores", "ChannelId", "dbo._DP_Channels");
            DropForeignKey("dbo._DP_BudgetsStore", "StoreId", "dbo._DP_Stores");
            DropForeignKey("dbo._DP_BudgetsChannel", "ChannelId", "dbo._DP_Channels");
            DropIndex("dbo.IdentityUserLogin", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaim", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            DropIndex("dbo.OrderLines", new[] { "ProductId" });
            DropIndex("dbo.OrderLines", new[] { "OrderId" });
            DropIndex("dbo._DP_BudgetsStore", new[] { "StoreId" });
            DropIndex("dbo._DP_Stores", new[] { "ChannelId" });
            DropIndex("dbo._DP_BudgetsChannel", new[] { "ChannelId" });
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.IdentityUserClaim");
            DropTable("dbo.IdentityUser");
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.IdentityRole");
            DropTable("dbo.Products");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderLines");
            DropTable("dbo.Customers");
            DropTable("dbo._DP_BudgetsStore");
            DropTable("dbo._DP_Stores");
            DropTable("dbo._DP_Channels");
            DropTable("dbo._DP_BudgetsChannel");
        }
    }
}
