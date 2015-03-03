using System.Collections.Generic;
using System.Linq;
using AutoMapper.QueryableExtensions;
using CQSLab.Business.Entities;
using CQSLab.Business.Queries.Configuration;
using CQSLab.Business.Queries.Result;
using CQSLab.CrossCutting.Enums;

namespace CQSLab.Business.Queries
{
    public class ChannelsQueries
    {
        private readonly ModelContext _context;

        public ChannelsQueries(ModelContext context)
        {
            _context = context;
        }

        public QueryResult<ChannelQueryResult> GetChannels(QueryConfiguration configuration)
        {
            AutoMapper.Mapper.CreateMap<Channel, ChannelQueryResult>()
                .ForMember(dst=>dst.NumberOfStores, opt=>opt.MapFrom(src=>src.Stores.Count));

            var query = _context.Channels
                .Project()
                .To<ChannelQueryResult>();

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
                            ? query.OrderBy(p => p.ChannelId)
                            : query.OrderByDescending(p => p.ChannelId);
                    break;
            }
            int count;
            
            IEnumerable<ChannelQueryResult> data;
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
            return new QueryResult<ChannelQueryResult>
            {
                Count = count,
                Configuration = configuration,
                Data = data
            };
        }
    }
}
