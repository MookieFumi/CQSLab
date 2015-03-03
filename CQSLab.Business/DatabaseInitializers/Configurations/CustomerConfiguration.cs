using System.Data.Entity.ModelConfiguration;
using CQSLab.Business.Entities;

namespace CQSLab.Business.DatabaseInitializers.Configurations
{
    public class CustomerConfiguration : EntityTypeConfiguration<Customer>
    {
        public CustomerConfiguration()
        {
            ToTable("Customers");
        }
    }
}
