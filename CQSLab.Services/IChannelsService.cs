using System.Collections.Generic;
using System.Threading.Tasks;
using CQSLab.Business.Entities;
using CQSLab.Business.Queries.Configuration;
using CQSLab.Business.Queries.Result;

namespace CQSLab.Services
{
    public interface IChannelsService
    {
        void AddChannel(Channel channel);
        Task<Channel> GetChannel(int channelId);
        Task<QueryResult<ChannelQueryResult>> GetChannels(QueryConfiguration configuration);
        void UpdateChannel(Channel channel);
        void RemoveChannel(int channelId);
        IEnumerable<int> GetBudgets(int channelId);
        Task<BudgetChannel> GetBudget(int channelId, int accountantPeriod);
        void UpdateBudget(BudgetChannel budget);
    }
}