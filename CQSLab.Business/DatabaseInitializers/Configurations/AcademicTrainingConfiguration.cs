using System.Data.Entity.ModelConfiguration;
using CQSLab.Business.Entities;

namespace CQSLab.Business.DatabaseInitializers.Configurations
{
    public class AcademicTrainingConfiguration : EntityTypeConfiguration<AcademicTraining>
    {
        public AcademicTrainingConfiguration()
        {
            ToTable("AcademicTrainings");
        }
    }
}