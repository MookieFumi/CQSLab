using System.Data.Entity.ModelConfiguration;
using CQSLab.Business.Entities;

namespace CQSLab.Business.DatabaseInitializers.Configurations
{
    public class StoreConfiguration : EntityTypeConfiguration<Store>
    {
        public StoreConfiguration()
        {
            ToTable("_DP_Stores");
        }
    }
}