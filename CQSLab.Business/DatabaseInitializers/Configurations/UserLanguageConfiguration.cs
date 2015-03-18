using System.Data.Entity.ModelConfiguration;
using CQSLab.Business.Entities;

namespace CQSLab.Business.DatabaseInitializers.Configurations
{
    public class UserLanguageConfiguration : EntityTypeConfiguration<UserLanguage>
    {
        public UserLanguageConfiguration()
        {
            ToTable("UserLanguages");
            HasKey(p => new {p.UserId, p.LanguageId, p.LevelLanguageId});
        }
    }
}