using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using CQSLab.Business;
using CQSLab.Business.Entities;
using CQSLab.Business.Queries;
using CQSLab.Business.Queries.Configuration;
using CQSLab.Business.Queries.Result;
using CQSLab.CrossCutting;
using EntityFramework.Extensions;

namespace CQSLab.Services.Impl
{
    public class ChannelsService : ServiceBase, IChannelsService
    {
        public ChannelsService(ModelContext context)
            : base(context)
        {
        }

        #region IChannelsService members

        public async void AddChannel(Channel channel)
        {
            using (var tran = new TransactionScope())
            {
                AddBudgetForCurrentPeriod(channel);
                Context.Channels.Add(channel);
                await Context.SaveChangesAsync();
                tran.Complete();
            }

        }

        public Task<BudgetChannel> GetBudget(int channelId, int accountantPeriod)
        {
            return Context.BudgetsChannel
                    .SingleOrDefaultAsync(p => p.ChannelId == channelId && p.AccountantPeriod == accountantPeriod);
        }

        public IEnumerable<int> GetBudgets(int channelId)
        {
            return Context.BudgetsChannel
                .Where(p => p.ChannelId == channelId)
                .Select(p => p.AccountantPeriod);
        }

        public Task<Channel> GetChannel(int channelId)
        {
            return Context.Channels
                .SingleOrDefaultAsync(p => p.ChannelId == channelId);
        }

        public async Task<QueryResult<ChannelQueryResult>> GetChannels(QueryConfiguration configuration)
        {
            var queries = new ChannelsQueries(Context);
            return await queries.GetChannels(configuration);
        }

        public async void RemoveChannel(int channelId)
        {
            var channel = await GetChannel(channelId);
            Context.Channels.Remove(channel);
            await Context.SaveChangesAsync();
        }

        public async void UpdateBudget(BudgetChannel budget)
        {
            using (var tran = new TransactionScope())
            {
                Context.BudgetsChannel.Attach(budget);
                Context.Entry(budget).State = EntityState.Modified;

                var budgetsStoreIds = await GetBudgetsStoreIdsWithSameData(budget.ChannelId, budget.AccountantPeriod);

                if (budgetsStoreIds.Any())
                {
                    Context.BudgetsStore
                        .Where(p => budgetsStoreIds.Contains(p.StoreId))
                        .UpdateAsync(t => new BudgetStore
                        {
                            January = budget.January,
                            February = budget.February,
                            March = budget.March,
                            April = budget.April,
                            May = budget.May,
                            June = budget.June,
                            July = budget.July,
                            August = budget.August,
                            September = budget.September,
                            October = budget.October,
                            November = budget.November,
                            December = budget.December
                        });

                }

                await Context.SaveChangesAsync();
                tran.Complete();
            }
        }

        public async Task<int> UpdateChannel(Channel channel)
        {
            Context.Channels.Attach(channel);
            Context.Entry(channel).State = EntityState.Modified;
            return await Context.SaveChangesAsync();
        }

        #endregion

        private static void AddBudgetForCurrentPeriod(Channel channel)
        {
            channel.Budgets.Add(new BudgetChannel
                                {
                                    AccountantPeriod = DateTime.Now.Year,
                                    January = Constants.DEFAULT_BUDGET_VALUE,
                                    February = Constants.DEFAULT_BUDGET_VALUE,
                                    March = Constants.DEFAULT_BUDGET_VALUE,
                                    April = Constants.DEFAULT_BUDGET_VALUE,
                                    May = Constants.DEFAULT_BUDGET_VALUE,
                                    June = Constants.DEFAULT_BUDGET_VALUE,
                                    July = Constants.DEFAULT_BUDGET_VALUE,
                                    August = Constants.DEFAULT_BUDGET_VALUE,
                                    September = Constants.DEFAULT_BUDGET_VALUE,
                                    October = Constants.DEFAULT_BUDGET_VALUE,
                                    November = Constants.DEFAULT_BUDGET_VALUE,
                                    December = Constants.DEFAULT_BUDGET_VALUE
                                });
        }

        private Task<BudgetChannel> GetBudgetChannelAsNoTracking(int channelId, int accountantPeriod)
        {
            return Context.BudgetsChannel
                .AsNoTracking()
                .SingleOrDefaultAsync(p =>
                    p.ChannelId == channelId &&
                    p.AccountantPeriod == accountantPeriod);
        }

        private async Task<IEnumerable<int>> GetBudgetsStoreIdsWithSameData(int channelId, int accountantPeriod)
        {
            var original = await GetBudgetChannelAsNoTracking(channelId, accountantPeriod);

            return Context.BudgetsStore.Where(p =>
                p.AccountantPeriod == original.AccountantPeriod &&
                p.January == original.January &&
                p.February == original.February &&
                p.March == original.March &&
                p.April == original.April &&
                p.May == original.May &&
                p.June == original.June &&
                p.July == original.July &&
                p.August == original.August &&
                p.September == original.September &&
                p.October == original.October &&
                p.November == original.November &&
                p.December == original.December
                ).Select(p => p.StoreId);
        }
    }
}