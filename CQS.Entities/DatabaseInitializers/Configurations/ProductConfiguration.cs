using System.Data.Entity.ModelConfiguration;

namespace CQS.Entities.DatabaseInitializers.Configurations
{
    public class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        {
            ToTable("Products");
        }
    }
}