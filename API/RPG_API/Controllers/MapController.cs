using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RPG_API.Data.Context;
using RPG_API.Models;

namespace RPG_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MapController : ControllerBase
    {
        private readonly APIContext _context;

        public MapController(APIContext context)
        {
            _context = context;
        }

        // GET: api/Map/Get/{id}
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<Map>> Get(int id)
        {
            Map map = await _context.Map.FindAsync(id);

            if (map == null)
            {
                return NotFound();
            }
            return map;
        }

        // GET: api/Map/GetAll
        [HttpGet("[action]")]
        public async Task<ActionResult<List<Map>>> GetAll()
        {
            List<Map> maps = await _context.Map.ToListAsync();

            if (maps == null || maps.FirstOrDefault() == null)
            {
                return NotFound();
            }
            return maps;
        }

        // PUT: api/Map/Update/{id}
        [HttpPut("[action]/{id}&{map}")]
        public async Task<IActionResult> Update(int id, [FromBody]Map map)
        {

            if (id != map.Id)
            {
                return BadRequest();
            }

            Map newMap = await _context.Map.FindAsync(id);
            if (newMap == null)
            {
                return NotFound();
            }
            newMap = map;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                BadRequest();
            }

            return Ok();
        }

        // POST: api/Map/Create
        [HttpPost("[action]/{map}")]
        public async Task<ActionResult<Map>> Create([FromBody] Map map)
        {
            _context.Map.Add(map);
            await _context.SaveChangesAsync();

            return Created(map.Id.ToString(), map);
        }

        // DELETE: api/Map/Delete/{id}
        [HttpDelete("[action]/{id}")]
        public async Task<ActionResult<Map>> Delete(int id)
        {
            Map map = await _context.Map.FindAsync(id);
            if (map == null)
            {
                return NotFound();
            }

            _context.Map.Remove(map);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
