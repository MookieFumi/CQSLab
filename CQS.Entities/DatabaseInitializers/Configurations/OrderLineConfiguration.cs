using System.Data.Entity.ModelConfiguration;

namespace CQS.Entities.DatabaseInitializers.Configurations
{
    public class OrderLineConfiguration : EntityTypeConfiguration<OrderLine>
    {
        public OrderLineConfiguration()
        {
            ToTable("OrderLines");
        }
    }
}