using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using CQSLab.Business.Entities;
using CQSLab.Business.Queries.Configuration;
using CQSLab.Business.Queries.Result;
using CQSLab.CrossCutting.Enums;

namespace CQSLab.Business.Queries
{
    public class ProductsQueries
    {
        private readonly ModelContext _context;

        public ProductsQueries(ModelContext context)
        {
            _context = context;
        }

        public async Task<QueryResult<ProductQueryResult>> GetProducts(QueryConfiguration configuration)
        {
            AutoMapper.Mapper.CreateMap<Product, ProductQueryResult>();

            var query = _context.Products
                .Project()
                .To<ProductQueryResult>();

            var sortDirection = configuration.Ordenation.SortDirection;
            switch (configuration.Ordenation.SortExpression.ToLower())
            {
                case "name":
                    query =
                        sortDirection == SortDirection.Ascending
                            ? query.OrderBy(p => p.Name)
                            : query.OrderByDescending(p => p.Name);
                    break;
                case "description":
                    query =
                        sortDirection == SortDirection.Ascending
                            ? query.OrderBy(p => p.Description)
                            : query.OrderByDescending(p => p.Description);
                    break;
                default:
                    query =
                        sortDirection == SortDirection.Ascending
                            ? query.OrderBy(p => p.ProductId)
                            : query.OrderByDescending(p => p.ProductId);
                    break;
            }
            int count;
            
            IEnumerable<ProductQueryResult> data;
            if (configuration.Paging.Enable)
            {
                count = await query.CountAsync();
                data = await query.Skip((configuration.Paging.PageIndex < 1 ? 0 : configuration.Paging.PageIndex - 1) *
                               configuration.Paging.PageSize)
                        .Take(configuration.Paging.PageSize)
                        .ToListAsync();
            }
            else
            {
                count = await query.CountAsync();
                data = await query.ToListAsync();
            }
            return new QueryResult<ProductQueryResult>
            {
                Count = count,
                Configuration = configuration,
                Data = data
            };
        }
    }
}
