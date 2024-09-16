using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RPG_API.Data.Context;
using RPG_API.Models.Base;

namespace RPG_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly APIContext _context;

        public ItemController(APIContext context)
        {
            _context = context;
        }

        //GET: api/Item/Get/{id}
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<Item>> Get(int id)
        {
            Item item = await _context.Item.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        //GET: api/Item/GetAll
        [HttpGet("[action]")]
        public async Task<ActionResult<List<Item>>> GetAll()
        {
            List<Item> items = await _context.Item.ToListAsync();

            if (items == null || items.FirstOrDefault() == null)
            {
                return NotFound();
            }
            return items;
        }

        //PUT: api/Item/Update/{id}
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]Item item)
        {

            if (id != item.Id)
            {
                return BadRequest();
            }

            Item newItem = await _context.Item.FindAsync(id);
            if (newItem == null)
            {
                return NotFound();
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

            return Created(item.Id.ToString(), item);
        }

        //POST: api/Item/Create
        [HttpPost("[action]/{item}")]
        public async Task<ActionResult<Item>> Create([FromBody]Item item)
        {
            _context.Item.Add(item);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

            return Created(item.Id.ToString(), item);
        }

        //DELETE: api/Item/Delete/{id}
        [HttpDelete("[action]/{id}")]
        public async Task<ActionResult<Item>> Delete(int id)
        {
            Item item = await _context.Item.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Item.Remove(item);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool ItemExists(int id)
        {
            return _context.Item.Any(e => e.Id == id);
        }
    }
}
