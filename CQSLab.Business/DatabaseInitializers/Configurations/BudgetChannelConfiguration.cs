using System.Data.Entity.ModelConfiguration;
using CQSLab.Business.Entities;

namespace CQSLab.Business.DatabaseInitializers.Configurations
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