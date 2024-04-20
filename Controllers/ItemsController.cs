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
        public async Task<IActionResult> GetAllItems([FromQuery] QueryParameters query)
        {
            IQueryable<Item> Items = _inventoryContext.Items.Skip(query.Size * (query.Page - 1)).Take(query.Size);
            return Ok(await Items.ToArrayAsync());
        }

        [HttpGet]
        [Route("GetItemById")]
        public async Task<IActionResult> GetItem(int Id)
        {

            try
            {
                Item item = _inventoryContext.Items.SingleOrDefault(x => x.Id == Id);

                if (item == null)
                {
                    return Ok("No Data Found.");
                }
                return Ok(item);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
