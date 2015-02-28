namespace CQSLab.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDPEntities : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo._DP_Stores", "ChannelId", "dbo._DP_Channels");
            DropForeignKey("dbo._DP_BudgetsStore", "StoreId", "dbo._DP_Stores");
            DropForeignKey("dbo._DP_BudgetsChannel", "ChannelId", "dbo._DP_Channels");
            DropIndex("dbo._DP_BudgetsStore", new[] { "StoreId" });
            DropIndex("dbo._DP_Stores", new[] { "ChannelId" });
            DropIndex("dbo._DP_BudgetsChannel", new[] { "ChannelId" });
            DropTable("dbo._DP_BudgetsStore");
            DropTable("dbo._DP_Stores");
            DropTable("dbo._DP_BudgetsChannel");
            DropTable("dbo._DP_Channels");
        }
    }
}
