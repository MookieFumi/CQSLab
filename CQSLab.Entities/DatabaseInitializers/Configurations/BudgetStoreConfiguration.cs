using System.Data.Entity.ModelConfiguration;

namespace CQSLab.Entities.DatabaseInitializers.Configurations
{
    public class BudgetStoreConfiguration : EntityTypeConfiguration<BudgetStore>
    {
        public BudgetStoreConfiguration()
        {
            ToTable("_DP_BudgetsStore");
            HasKey(p => new { p.StoreId, p.AccountantPeriod });
        }
    }
}