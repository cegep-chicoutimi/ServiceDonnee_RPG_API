using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RPG_API.Data.Context;
using RPG_API.Models;

namespace RPG_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonsterController : ControllerBase
    {
        private readonly APIContext _context;

        public MonsterController(APIContext context)
        {
            _context = context;
        }

        // GET: api/Monster/Get/{id}
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<Monster>> Get(int id)
        {
            Monster monster = await _context.Monster.FindAsync(id);

            if (monster == null)
            {
                return NotFound();
            }
            return monster;
        }

        // GET: api/Monster/GetAll
        [HttpGet("[action]")]
        public async Task<ActionResult<List<Monster>>> GetAll()
        {
            List<Monster> monsters = await _context.Monster.ToListAsync();

            if (monsters == null || monsters.FirstOrDefault() == null)
            {
                return NotFound();
            }
            return monsters;
        }

        // PUT: api/Monster/Update/{id}
        [HttpPut("[action]/{id}&{monster}")]
        public async Task<IActionResult> Update(int id, [FromBody]Monster monster)
        {

            if (id != monster.Id)
            {
                return BadRequest();
            }

            Monster newMonster = await _context.Monster.FindAsync(id);
            if (newMonster == null)
            {
                return NotFound();
            }
            newMonster = monster;

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

        // POST: api/Monster/Create
        [HttpPost("[action]/{monster}")]
        public async Task<ActionResult<Monster>> Create([FromBody] Monster monster)
        {
            _context.Monster.Add(monster);
            await _context.SaveChangesAsync();

            return Created(monster.Id.ToString(), monster);
        }

        // DELETE: api/Monster/Delete/{id}
        [HttpDelete("[action]/{id}")]
        public async Task<ActionResult<Monster>> Delete(int id)
        {
            Monster monster = await _context.Monster.FindAsync(id);
            if (monster == null)
            {
                return NotFound();
            }

            _context.Monster.Remove(monster);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
