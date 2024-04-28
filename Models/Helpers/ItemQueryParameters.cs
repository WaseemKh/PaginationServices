namespace Pagination_API.Models.Helpers
{
    public class ItemQueryParameters:QueryParameters
    {
        public Decimal? MinPrice { get; set; }
        public Decimal? MaxPrice { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Barcode { get; set; } = string.Empty;
    }
}
