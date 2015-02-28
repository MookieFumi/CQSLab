using AutoMapper;
using CQSLab.Entities;
using CQSLab.Entities.Queries;
using CQSLab.Entities.Queries.Configuration;
using CQSLab.Entities.Queries.Result;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Transactions;

namespace CQSLab.Services
{
    public class StoresService : ServiceBase, IStoresService
    {
        public StoresService(ModelContext context)
            : base(context)
        {
        }

        public void AddStore(Store store)
        {
            using (var tran = new TransactionScope())
            {
                AddBudgetForCurrentPeriod(store);
                Context.Stores.Add(store);
                Context.SaveChanges();
                tran.Complete();
            }
        }

        private void AddBudgetForCurrentPeriod(Store store)
        {
            Mapper.CreateMap<BudgetChannel, BudgetStore>();
            var budgetChannel = Context.BudgetsChannel.FirstOrDefault(p => p.AccountantPeriod == DateTime.Now.Year);
            var budgetStore = AutoMapper.Mapper.Map<BudgetChannel, BudgetStore>(budgetChannel);
            store.Budgets.Add(budgetStore);
        }

        public Store GetStore(int storeId)
        {
            return Context.Stores.SingleOrDefault(p => p.StoreId == storeId);
        }

        public QueryResult<StoreQueryResult> GetStores(QueryConfiguration configuration)
        {
            var queries = new StoresQueries(Context);
            return queries.GetStores(configuration);
        }

        public void UpdateStore(Store store)
        {
            Context.Stores.Attach(store);
            Context.Entry(store).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public void RemoveStore(int storeId)
        {
            var store = GetStore(storeId);
            Context.Stores.Remove(store);
            Context.SaveChanges();
        }

        public Dictionary<int, string> GetChannels()
        {
            return Context.Channels
                .Select(p => new { p.ChannelId, p.Name })
                .ToDictionary(prop => prop.ChannelId, prop => prop.Name);
        }
    }
}