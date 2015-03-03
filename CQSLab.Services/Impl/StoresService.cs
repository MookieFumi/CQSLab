using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Transactions;
using AutoMapper;
using CQSLab.Business;
using CQSLab.Business.Entities;
using CQSLab.Business.Queries;
using CQSLab.Business.Queries.Configuration;
using CQSLab.Business.Queries.Result;

namespace CQSLab.Services.Impl
{
    public class StoresService : ServiceBase, IStoresService
    {
        public StoresService(ModelContext context)
            : base(context)
        {
        }

        #region IStoresService members

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

        public Dictionary<int, string> GetChannels()
        {
            return Context.Channels
                .Select(p => new { p.ChannelId, p.Name })
                .ToDictionary(prop => prop.ChannelId, prop => prop.Name);
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

        public void RemoveStore(int storeId)
        {
            var store = GetStore(storeId);
            Context.Stores.Remove(store);
            Context.SaveChanges();
        }

        public void UpdateStore(Store store)
        {
            Context.Stores.Attach(store);
            Context.Entry(store).State = EntityState.Modified;
            Context.SaveChanges();
        }

        #endregion

        private void AddBudgetForCurrentPeriod(Store store)
        {
            Mapper.CreateMap<BudgetChannel, BudgetStore>();
            var budgetChannel = Context.BudgetsChannel.FirstOrDefault(p => p.AccountantPeriod == DateTime.Now.Year);
            var budgetStore = Mapper.Map<BudgetChannel, BudgetStore>(budgetChannel);
            store.Budgets.Add(budgetStore);
        }
    }
}