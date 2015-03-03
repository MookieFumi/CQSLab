namespace CQSLab.Business.Queries.Configuration
{
    public class QueryConfiguration
    {
        public QueryConfiguration()
        {
            Paging = new PagingConfiguration();
            Ordenation = new OrdernationConfiguration();
        }

        public OrdernationConfiguration Ordenation { get; set; }
        public PagingConfiguration Paging { get; set; }
    }
}