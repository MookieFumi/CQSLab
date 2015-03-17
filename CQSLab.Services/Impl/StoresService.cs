using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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

        public async void AddStore(Store store)
        {
            using (var tran = new TransactionScope())
            {
                AddBudgetForCurrentPeriod(store);
                Context.Stores.Add(store);
                await Context.SaveChangesAsync();
                tran.Complete();
            }
        }

        public Task<Dictionary<int, string>> GetChannels()
        {
            return Context.Channels
                .Select(p => new { p.ChannelId, p.Name })
                .ToDictionaryAsync(prop => prop.ChannelId, prop => prop.Name);
        }

        public Task<Store> GetStore(int storeId)
        {
            return Context.Stores
                .SingleOrDefaultAsync(p => p.StoreId == storeId);
        }

        public async Task<QueryResult<StoreQueryResult>> GetStores(QueryConfiguration configuration)
        {
            var queries = new StoresQueries(Context);
            return await queries.GetStores(configuration);
        }

        public async void RemoveStore(int storeId)
        {
            var store = await GetStore(storeId);
            Context.Stores.Remove(store);
            await Context.SaveChangesAsync();
        }

        public async void UpdateStore(Store store)
        {
            Context.Stores.Attach(store);
            Context.Entry(store).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }

        #endregion

        private async void AddBudgetForCurrentPeriod(Store store)
        {
            Mapper.CreateMap<BudgetChannel, BudgetStore>();
            var budgetChannel = await Context.BudgetsChannel.FirstOrDefaultAsync(p => p.AccountantPeriod == DateTime.Now.Year);
            var budgetStore = Mapper.Map<BudgetChannel, BudgetStore>(budgetChannel);
            store.Budgets.Add(budgetStore);
        }
    }
}