using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RPG_API.Data.Context;
using RPG_API.Models;

namespace RPG_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TileController : Controller
    {
        private readonly APIContext _context;

        public TileController(APIContext context)
        {
            _context = context;
        }

        // GET: api/Tile/Get/{id}
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<Tile>> Get(int id)
        {
            Tile tile = await _context.Tile.FindAsync(id);

            if (tile == null)
            {
                return NotFound();
            }
            return tile;
        }

        // GET: api/Tile/GetAll
        [HttpGet("[action]")]
        public async Task<ActionResult<List<Tile>>> GetAll()
        {
            List<Tile> tiles = await _context.Tile.ToListAsync();

            if (tiles == null || tiles.FirstOrDefault() == null)
            {
                return NotFound();
            }
            return tiles;
        }

        // PUT: api/Tile/Update/{id}
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Tile tile)
        {

            if (id != tile.Id)
            {
                return BadRequest();
            }

            Tile newTile = await _context.Tile.FindAsync(id);
            if (newTile == null)
            {
                return NotFound();
            }
            newTile = tile;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                BadRequest();
            }

            return NoContent();
        }

        // POST: api/Tile/Create
        [HttpPost("[action]/{tile}")]
        public async Task<ActionResult<Tile>> Create([FromBody] Tile tile)
        {
            _context.Tile.Add(tile);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = tile.Id }, tile);
        }

        // DELETE: api/Tile/Delete/{id}
        [HttpDelete("[action]/{id}")]
        public async Task<ActionResult<Tile>> Delete(int id)
        {
            Tile tile = await _context.Tile.FindAsync(id);
            if (tile == null)
            {
                return NotFound();
            }

            _context.Tile.Remove(tile);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
