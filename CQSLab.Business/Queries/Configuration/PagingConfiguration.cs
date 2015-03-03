using CQSLab.CrossCutting;

namespace CQSLab.Business.Queries.Configuration
{
    public class PagingConfiguration
    {
        public PagingConfiguration()
        {
            Enable = true;
            PageIndex = Constants.DEFAULT_PAGE_INDEX;
            PageSize = Constants.DEFAULT_PAGE_SIZE;
            FallbackPage = false;
        }

        public int PageIndex { get; set; }
        public bool Enable { get; set; }
        public bool FallbackPage { get; set; }
        public int PageSize { get; set; }
    }
}