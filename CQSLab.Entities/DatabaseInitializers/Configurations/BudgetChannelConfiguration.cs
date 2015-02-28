using System.Data.Entity.ModelConfiguration;

namespace CQSLab.Entities.DatabaseInitializers.Configurations
{
    public class BudgetChannelConfiguration : EntityTypeConfiguration<BudgetChannel>
    {
        public BudgetChannelConfiguration()
        {
            ToTable("_DP_BudgetsChannel");
            HasKey(p => new { p.ChannelId, p.AccountantPeriod });
        }
    }
}