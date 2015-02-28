using System.Collections.Generic;
using CQSLab.Entities;
using CQSLab.Entities.Queries.Configuration;
using CQSLab.Entities.Queries.Result;

namespace CQSLab.Services
{
    public interface IChannelsService
    {
        void AddChannel(Channel channel);
        Channel GetChannel(int channelId);
        QueryResult<ChannelQueryResult> GetChannels(QueryConfiguration configuration);
        void UpdateChannel(Channel channel);
        void RemoveChannel(int channelId);
        IEnumerable<int> GetBudgets(int channelId);
        BudgetChannel GetBudget(int channelId, int accountantPeriod);
        void UpdateBudget(BudgetChannel budget);
    }
}