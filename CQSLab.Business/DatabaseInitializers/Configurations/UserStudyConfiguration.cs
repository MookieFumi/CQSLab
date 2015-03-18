using System;
using System.Data.Entity.ModelConfiguration;
using CQSLab.Business.Entities;

namespace CQSLab.Business.DatabaseInitializers.Configurations
{
    public class UserStudyConfiguration : EntityTypeConfiguration<UserStudy>
    {
        public UserStudyConfiguration()
        {
            ToTable("UserStudies");
            HasKey(p => new { p.UserId, p.AcademicLevelId, p.AcademicTrainingId });
            Property(p => p.Comments).HasMaxLength(Int32.MaxValue);
        }
    }
}