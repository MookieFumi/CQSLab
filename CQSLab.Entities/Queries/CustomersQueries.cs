using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using CQSLab.CrossCutting.Enums;
using CQSLab.Entities.Queries.Configuration;
using CQSLab.Entities.Queries.Result;

namespace CQSLab.Entities.Queries
{
    public class CustomersQueries
    {
        private readonly ModelContext _context;

        public CustomersQueries(ModelContext context)
        {
            _context = context;
        }

        public QueryResult<CustomerQueryResult> GetCustomers(QueryConfiguration configuration)
        {
            AutoMapper.Mapper.CreateMap<Customer, CustomerQueryResult>();

            var query = _context.Customers
                .Project()
                .To<CustomerQueryResult>();

            var sortDirection = configuration.Ordenation.SortDirection;
            switch (configuration.Ordenation.SortExpression.ToLower())
            {
                case "name":
                    query =
                        sortDirection == SortDirection.Ascending
                            ? query.OrderBy(p => p.Name)
                            : query.OrderByDescending(p => p.Name);
                    break;
                case "surname":
                    query =
                        sortDirection == SortDirection.Ascending
                            ? query.OrderBy(p => p.Surname)
                            : query.OrderByDescending(p => p.Surname);
                    break;
                default:
                    query =
                        sortDirection == SortDirection.Ascending
                            ? query.OrderBy(p => p.CustomerId)
                            : query.OrderByDescending(p => p.CustomerId);
                    break;
            }
            int count;
            
            IEnumerable<CustomerQueryResult> data;
            if (configuration.Paging.Enable)
            {
                count = query.Count();
                data =
                    query.Skip((configuration.Paging.PageIndex < 1 ? 0 : configuration.Paging.PageIndex - 1) *
                               configuration.Paging.PageSize)
                        .Take(configuration.Paging.PageSize)
                        .ToList();
            }
            else
            {
                data = query.ToList();
                count = data.Count();
            }
            return new QueryResult<CustomerQueryResult>
            {
                Count = count,
                Configuration = configuration,
                Data = data
            };
        }
    }
}
