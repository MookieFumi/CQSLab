using System.Collections.Generic;
using CQSLab.Business.Entities;
using CQSLab.Business.Queries.Configuration;
using CQSLab.Business.Queries.Result;

namespace CQSLab.Services
{
    public interface IStoresService
    {
        void AddStore(Store store);
        Store GetStore(int storeId);
        QueryResult<StoreQueryResult> GetStores(QueryConfiguration configuration);
        void UpdateStore(Store store);
        void RemoveStore(int storeId);
        Dictionary<int, string> GetChannels();
    }
}