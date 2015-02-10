using System.Data.Entity.ModelConfiguration;

namespace CQSLab.Entities.DatabaseInitializers.Configurations
{
    public class CustomerConfiguration : EntityTypeConfiguration<Customer>
    {
        public CustomerConfiguration()
        {
            ToTable("Customers");
        }
    }
}
