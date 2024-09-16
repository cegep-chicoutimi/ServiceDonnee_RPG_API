using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RPG_API.Data.Context;
using RPG_API.Models;

namespace RPG_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestController : ControllerBase
    {
        private readonly APIContext _context;

        public QuestController(APIContext context)
        {
            _context = context;
        }

        // GET: api/Quest/Get/{id}
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<Quest>> Get(int id)
        {
            Quest quest = await _context.Quest.FindAsync(id);

            if (quest == null)
            {
                return NotFound();
            }
            return quest;
        }

        // GET: api/Quest/GetAll
        [HttpGet("[action]")]
        public async Task<ActionResult<List<Quest>>> GetAll()
        {
            List<Quest> quests = await _context.Quest.ToListAsync();

            if (quests == null || quests.FirstOrDefault() == null)
            {
                return NotFound();
            }
            return quests;
        }

        // PUT: api/Quest/Update/{id}
        [HttpPut("[action]/{id}&{quest}")]
        public async Task<IActionResult> Update(int id, [FromBody] Quest quest)
        {

            if (id != quest.Id)
            {
                return BadRequest();
            }

            Quest newQuest = await _context.Quest.FindAsync(id);
            if (newQuest == null)
            {
                return NotFound();
            }
            newQuest = quest;

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

        // POST: api/Quest/Create
        [HttpPost("[action]/{quest}")]
        public async Task<ActionResult<Quest>> Create([FromBody] Quest quest)
        {
            _context.Quest.Add(quest);
            await _context.SaveChangesAsync();

            return Created(quest.Id.ToString(), quest);
        }

        // DELETE: api/Quest/Delete/{id}
        [HttpDelete("[action]/{id}")]
        public async Task<ActionResult<Quest>> Delete(int id)
        {
            Quest quest = await _context.Quest.FindAsync(id);

            if (quest == null)
            {
                return NotFound();
            }

            _context.Quest.Remove(quest);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
