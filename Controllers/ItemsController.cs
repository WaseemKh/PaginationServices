using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pagination_API.Models;
using Pagination_API.Models.EFModels;
using Pagination_API.Models.Helpers;

namespace Pagination_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly InventoryContext _inventoryContext;
        public ItemsController(InventoryContext inventoryContext)
        {
            _inventoryContext = inventoryContext;
        }

        [HttpGet]
        [Route("GetAllItems")]
        public async Task<IActionResult> GetAllItems([FromQuery] ItemQueryParameters queryParams)
        {

            
            if (queryParams.Size < 1)
            {
                return BadRequest("Size parameter must be at least 1.");
            }

           
            if (queryParams.Page < 0)
            {
                return BadRequest("Page parameter must not be negative.");
            }




            IQueryable<Item> Items = _inventoryContext.Items.AsNoTracking();

            if (queryParams.MinPrice != null)
            {
                Items = Items.Where(x => x.Price >= queryParams.MinPrice);
            }
            if (queryParams.MaxPrice != null)
            {
                Items = Items.Where(x => x.Price <= queryParams.MaxPrice);
            }
            if (!string.IsNullOrEmpty(queryParams.Barcode))
            {
                Items = Items.Where(x => x.Barcode == queryParams.Barcode);
            }
            if (!string.IsNullOrEmpty(queryParams.Name))
            {
                Items = Items.Where(x => x.Name.ToLower().Contains(queryParams.Name.ToLower()));

            }

            if (!string.IsNullOrEmpty(queryParams.SortBy))
            {

                if (typeof(Item).GetProperty(queryParams.SortBy) != null)
                {
                    Items = Items.OrderByCustom(
                        queryParams.SortBy,
                        queryParams.SortOrder);
                }
            }
            var totalItems = await Items.CountAsync();
            var skipAmount = queryParams.Page <= 1 ? 0 : queryParams.Size * (queryParams.Page - 1);

            var items = await Items

         .Skip(skipAmount)
         .Take(queryParams.Size)
         .ToArrayAsync();


            var response = new PagedResponse<Item>
            {
                Data = items,
                PageNumber = queryParams.Page,
                PageSize = queryParams.Size,
                TotalRecords = totalItems,
                TotalPages = (int)Math.Ceiling((double)totalItems / queryParams.Size)
            };
            return Ok(response);
        }

        public class PagedResponse<T>
        {
            public IEnumerable<T> Data { get; set; }
            public int PageNumber { get; set; }
            public int PageSize { get; set; }
            public int TotalRecords { get; set; }
            public int TotalPages { get; set; }
        }

        [HttpGet]
        [Route("GetItemById/{id}")]
        public async Task<IActionResult> GetItem(int id)
        {

            // var item = await _inventoryContext.Items.FindAsync(id);
            var item = await _inventoryContext.Items.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (item == null)
            {
                return Ok("Item not found.");
            }
            return Ok(item);

        }



    }

}
