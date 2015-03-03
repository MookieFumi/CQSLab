using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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

        public void AddChannel(Channel channel)
        {
            using (var tran = new TransactionScope())
            {
                AddBudgetForCurrentPeriod(channel);
                Context.Channels.Add(channel);
                Context.SaveChanges();
                tran.Complete();
            }
        }

        public BudgetChannel GetBudget(int channelId, int accountantPeriod)
        {
            return
                Context.BudgetsChannel
                    .SingleOrDefault(p => p.ChannelId == channelId && p.AccountantPeriod == accountantPeriod);
        }

        public IEnumerable<int> GetBudgets(int channelId)
        {
            return Context.BudgetsChannel
                .Where(p => p.ChannelId == channelId)
                .Select(p => p.AccountantPeriod);
        }

        public Channel GetChannel(int channelId)
        {
            return Context.Channels.SingleOrDefault(p => p.ChannelId == channelId);
        }

        public QueryResult<ChannelQueryResult> GetChannels(QueryConfiguration configuration)
        {
            var queries = new ChannelsQueries(Context);
            return queries.GetChannels(configuration);
        }

        public void RemoveChannel(int channelId)
        {
            var channel = GetChannel(channelId);
            Context.Channels.Remove(channel);
            Context.SaveChanges();
        }

        public void UpdateBudget(BudgetChannel budget)
        {
            using (var tran = new TransactionScope())
            {
                Context.BudgetsChannel.Attach(budget);
                Context.Entry(budget).State = EntityState.Modified;

                var budgetsStoreIds = GetBudgetsStoreIdsWithSameData(budget.ChannelId, budget.AccountantPeriod).ToList();

                Context.BudgetsStore
                    .Where(p => budgetsStoreIds.Contains(p.StoreId))
                    .Update(t => new BudgetStore
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
                Context.SaveChanges();
                tran.Complete();
            }
        }

        public void UpdateChannel(Channel channel)
        {
            Context.Channels.Attach(channel);
            Context.Entry(channel).State = EntityState.Modified;
            Context.SaveChanges();
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

        private BudgetChannel GetBudgetChannelAsNoTracking(int channelId, int accountantPeriod)
        {
            return Context.BudgetsChannel
                .AsNoTracking()
                .SingleOrDefault(p =>
                    p.ChannelId == channelId &&
                    p.AccountantPeriod == accountantPeriod);
        }

        private IEnumerable<int> GetBudgetsStoreIdsWithSameData(int channelId, int accountantPeriod)
        {
            var original = GetBudgetChannelAsNoTracking(channelId, accountantPeriod);

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