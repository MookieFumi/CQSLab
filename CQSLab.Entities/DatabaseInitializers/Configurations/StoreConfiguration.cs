using System.Data.Entity.ModelConfiguration;

namespace CQSLab.Entities.DatabaseInitializers.Configurations
{
    public class StoreConfiguration : EntityTypeConfiguration<Store>
    {
        public StoreConfiguration()
        {
            ToTable("_DP_Stores");
        }
    }
}