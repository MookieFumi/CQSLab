using System.Data.Entity.ModelConfiguration;
using CQSLab.Business.Entities;

namespace CQSLab.Business.DatabaseInitializers.Configurations
{
    public class LevelLanguageConfiguration : EntityTypeConfiguration<LevelLanguage>
    {
        public LevelLanguageConfiguration()
        {
            ToTable("LevelLanguages");
        }
    }
}