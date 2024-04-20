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
        public async Task<IActionResult> GetAllItems([FromQuery] ItemQueryParameters query)
        {
            if (query.Page < 1 || query.Size < 1)
            {
                return BadRequest("Invalid page or size parameter.");
            }
            IQueryable<Item> Items = _inventoryContext.Items.AsNoTracking();
            if (query.MinPrice != null)
            {
                Items = Items.Where(x => x.Price >= query.MinPrice);
            }
            if (query.MaxPrice != null)
            {
                Items = Items.Where(x => x.Price <= query.MaxPrice);
            }
            var totalItems = await Items.CountAsync();
            var items = await Items
         .OrderBy(x => x.Id)
         .Skip(query.Size * (query.Page - 1))
         .Take(query.Size)
         .ToArrayAsync();


            var response = new PagedResponse<Item>
            {
                Data = items,
                PageNumber = query.Page,
                PageSize = query.Size,
                TotalRecords = totalItems,
                TotalPages = (int)Math.Ceiling((double)totalItems / query.Size)
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
            var item = await _inventoryContext.Items.AsNoTracking().FirstOrDefaultAsync(x=>x.Id ==id);


            if (item == null)
            {
                return Ok("Item not found.");
            }
            return Ok(item);

        }



    }

}
