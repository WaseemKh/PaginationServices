using System;
using System.Collections.Generic;

namespace Pagination_API.Models
{
    public partial class Item
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Barcode { get; set; }
        public decimal? Price { get; set; }
    }
}
