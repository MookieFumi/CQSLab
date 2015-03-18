using System.Data.Entity.ModelConfiguration;
using CQSLab.Business.Entities;

namespace CQSLab.Business.DatabaseInitializers.Configurations
{
    public class AcademicLevelConfiguration : EntityTypeConfiguration<AcademicLevel>
    {
        public AcademicLevelConfiguration()
        {
            ToTable("AcademicLevels");
        }
    }
}