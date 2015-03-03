using System.Collections.Generic;
using CQSLab.Business.Queries.Configuration;

namespace CQSLab.Business.Queries.Result
{
    public class QueryResult<T>
    {
        public int Count { get; set; }
        public QueryConfiguration Configuration { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}
