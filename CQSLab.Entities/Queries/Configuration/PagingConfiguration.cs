namespace CQSLab.Entities.Queries.Configuration
{
    public class PagingConfiguration
    {
        public PagingConfiguration()
        {
            Enable = true;
            PageIndex = 1;
            PageSize = 10;
            FallbackPage = false;
        }

        public int PageIndex { get; set; }
        public bool Enable { get; set; }
        public bool FallbackPage { get; set; }
        public int PageSize { get; set; }
    }
}