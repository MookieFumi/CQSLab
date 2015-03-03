using System.Data.Entity.ModelConfiguration;
using CQSLab.Business.Entities;

namespace CQSLab.Business.DatabaseInitializers.Configurations
{
    public class ChannelConfiguration : EntityTypeConfiguration<Channel>
    {
        public ChannelConfiguration()
        {
            ToTable("_DP_Channels");
        }
    }
}