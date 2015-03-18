namespace CQSLab.Business.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLanguagesEntites : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserLanguages",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LanguageId = c.Int(nullable: false),
                        LevelLanguageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.LanguageId, t.LevelLanguageId })
                .ForeignKey("dbo.Languages", t => t.LanguageId)
                .ForeignKey("dbo.LevelLanguages", t => t.LevelLanguageId)
                .ForeignKey("dbo.IdentityUser", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.LanguageId)
                .Index(t => t.LevelLanguageId);
            
            CreateTable(
                "dbo.Languages",
                c => new
                    {
                        LanguageId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 450),
                    })
                .PrimaryKey(t => t.LanguageId);
            
            CreateTable(
                "dbo.LevelLanguages",
                c => new
                    {
                        LevelLanguageId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 450),
                    })
                .PrimaryKey(t => t.LevelLanguageId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserLanguages", "UserId", "dbo.IdentityUser");
            DropForeignKey("dbo.UserLanguages", "LevelLanguageId", "dbo.LevelLanguages");
            DropForeignKey("dbo.UserLanguages", "LanguageId", "dbo.Languages");
            DropIndex("dbo.UserLanguages", new[] { "LevelLanguageId" });
            DropIndex("dbo.UserLanguages", new[] { "LanguageId" });
            DropIndex("dbo.UserLanguages", new[] { "UserId" });
            DropTable("dbo.LevelLanguages");
            DropTable("dbo.Languages");
            DropTable("dbo.UserLanguages");
        }
    }
}
