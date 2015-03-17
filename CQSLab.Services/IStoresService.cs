using System.Collections.Generic;
using System.Threading.Tasks;
using CQSLab.Business.Entities;
using CQSLab.Business.Queries.Configuration;
using CQSLab.Business.Queries.Result;

namespace CQSLab.Services
{
    public interface IStoresService
    {
        void AddStore(Store store);
        Task<Store> GetStore(int storeId);
        Task<QueryResult<StoreQueryResult>> GetStores(QueryConfiguration configuration);
        void UpdateStore(Store store);
        void RemoveStore(int storeId);
        Task<Dictionary<int, string>> GetChannels();
    }
}