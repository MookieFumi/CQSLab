namespace CQSLab.Business.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStudiesEntites : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AcademicLevels",
                c => new
                    {
                        AcademicLevelId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 450),
                    })
                .PrimaryKey(t => t.AcademicLevelId);
            
            CreateTable(
                "dbo.AcademicTrainings",
                c => new
                    {
                        AcademicTrainingId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 450),
                        AcademicLevelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AcademicTrainingId)
                .ForeignKey("dbo.AcademicLevels", t => t.AcademicLevelId)
                .Index(t => t.AcademicLevelId);
            
            CreateTable(
                "dbo.UserStudies",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        AcademicLevelId = c.Int(nullable: false),
                        AcademicTrainingId = c.Int(nullable: false),
                        Comments = c.String(),
                    })
                .PrimaryKey(t => new { t.UserId, t.AcademicLevelId, t.AcademicTrainingId })
                .ForeignKey("dbo.AcademicLevels", t => t.AcademicLevelId)
                .ForeignKey("dbo.AcademicTrainings", t => t.AcademicTrainingId)
                .ForeignKey("dbo.IdentityUser", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.AcademicLevelId)
                .Index(t => t.AcademicTrainingId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserStudies", "UserId", "dbo.IdentityUser");
            DropForeignKey("dbo.UserStudies", "AcademicTrainingId", "dbo.AcademicTrainings");
            DropForeignKey("dbo.UserStudies", "AcademicLevelId", "dbo.AcademicLevels");
            DropForeignKey("dbo.AcademicTrainings", "AcademicLevelId", "dbo.AcademicLevels");
            DropIndex("dbo.UserStudies", new[] { "AcademicTrainingId" });
            DropIndex("dbo.UserStudies", new[] { "AcademicLevelId" });
            DropIndex("dbo.UserStudies", new[] { "UserId" });
            DropIndex("dbo.AcademicTrainings", new[] { "AcademicLevelId" });
            DropTable("dbo.UserStudies");
            DropTable("dbo.AcademicTrainings");
            DropTable("dbo.AcademicLevels");
        }
    }
}
