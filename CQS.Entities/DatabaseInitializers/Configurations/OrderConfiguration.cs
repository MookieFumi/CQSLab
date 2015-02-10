using System.Data.Entity.ModelConfiguration;

namespace CQS.Entities.DatabaseInitializers.Configurations
{
    public class OrderConfiguration : EntityTypeConfiguration<Order>
    {
        public OrderConfiguration()
        {
            ToTable("Orders");
        }
    }
}