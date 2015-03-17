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
    public class StoresQueries
    {
        private readonly ModelContext _context;

        public StoresQueries(ModelContext context)
        {
            _context = context;
        }

        public async Task<QueryResult<StoreQueryResult>> GetStores(QueryConfiguration configuration)
        {
            AutoMapper.Mapper.CreateMap<Store, StoreQueryResult>()
                .ForMember(dst=>dst.ChannelId, opt=>opt.MapFrom(src=>src.ChannelId))
                .ForMember(dst => dst.ChannelName, opt => opt.MapFrom(src => src.Channel.Name));

            var query = _context.Stores
                .Project()
                .To<StoreQueryResult>();

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
                            ? query.OrderBy(p => p.StoreId)
                            : query.OrderByDescending(p => p.StoreId);
                    break;
            }
            
            int count;
            List<StoreQueryResult> data;

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
                data = await query.ToListAsync();
                count = await query.CountAsync();
            }

            return new QueryResult<StoreQueryResult>
            {
                Count = count,
                Configuration = configuration,
                Data = data
            };
        }
    }
}
