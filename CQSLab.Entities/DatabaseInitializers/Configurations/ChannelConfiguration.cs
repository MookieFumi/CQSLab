using System.Data.Entity.ModelConfiguration;

namespace CQSLab.Entities.DatabaseInitializers.Configurations
{
    public class ChannelConfiguration : EntityTypeConfiguration<Channel>
    {
        public ChannelConfiguration()
        {
            ToTable("_DP_Channels");
        }
    }
}