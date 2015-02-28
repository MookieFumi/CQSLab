using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQSLab.Entities.Queries.Configuration;

namespace CQSLab.Entities.Queries.Result
{
    public class QueryResult<T>
    {
        public int Count { get; set; }
        public QueryConfiguration Configuration { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}
