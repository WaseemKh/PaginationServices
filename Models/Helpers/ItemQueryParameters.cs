namespace Pagination_API.Models.Helpers
{
    public class ItemQueryParameters:QueryParameters
    {
        public Decimal? MinPrice { get; set; }
        public Decimal? MaxPrice { get; set; }
    }
}
