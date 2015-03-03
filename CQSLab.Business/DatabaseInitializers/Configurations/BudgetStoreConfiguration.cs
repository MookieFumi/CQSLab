using System.Data.Entity.ModelConfiguration;
using CQSLab.Business.Entities;

namespace CQSLab.Business.DatabaseInitializers.Configurations
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