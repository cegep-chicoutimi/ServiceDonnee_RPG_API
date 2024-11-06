using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RPG_API.Data.Context;
using RPG_API.Models;
using RPG_API.Models.Base;

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
            return Ok(quest);
        }
        [HttpGet("[action]/{title}")]
        public async Task<ActionResult<Quest>> GetByTitle(string title)
        {
            Quest quest = await _context.Quest.FirstOrDefaultAsync(q => q.Title == title);

            if (quest == null)
            {
                return NotFound();
            }


            return Ok(quest);
        }
        //TODO: Filtres
        [HttpGet("[action]/{description}")]
        public async Task<ActionResult<Quest>> GetByDescription(string description)
        {
            Quest quest = await _context.Quest.FirstOrDefaultAsync(q => q.Description == description);
            
            if (quest == null) 
            {
                return NotFound();
            }
            
            
            return Ok(quest);
        }

        // GET: api/Quest/GetAll
        [HttpGet("[action]")]
        public async Task<ActionResult<PaginatedList<Quest>>> GetAll(int? pageNumber = 1, int pageSize = 10)
        {
             var quests =  _context.Quest.AsQueryable();

            var totalCount = await quests.CountAsync();

            if (totalCount == 0)
            {
                return NotFound("Aucune quête de ce type n'a été trouvé.");
            }

            var pagetTiles = await PaginatedList<Quest>.CreateAsync(quests.AsNoTracking(), pageNumber ?? 1, pageSize);

            return Ok(pagetTiles);
        }
        [HttpGet("[action]")]
        public async Task<ActionResult<PaginatedList<Quest>>> SearchQuestByTitle(string? firstLetter, string? nameContains, int? pageNumber = 1, int pageSize = 10)
        {
            var quests = _context.Quest.AsQueryable();

            // Appliquer le filtre pour le titre qui commence par la première lettre spécifique
            if (!string.IsNullOrEmpty(firstLetter))
            {
                quests = quests.Where(i => i.Title.StartsWith(firstLetter));
            }

            if (!string.IsNullOrEmpty(nameContains))
            {
                quests = quests.Where(i => EF.Functions.Like(i.Title, $"%{nameContains}%"));
            }


            // Calculer le nombre total avant la pagination
            var totalCount = await quests.CountAsync();

            // Utiliser PaginatedList pour créer une liste paginée
            var pagedQuests = await PaginatedList<Quest>.CreateAsync(quests.AsNoTracking(), pageNumber ?? 1, pageSize);

            if (totalCount == 0)
            {
                return NotFound("Aucune quête n'a été trouvé qui correspond à ces critères.");
            }
            return Ok(pagedQuests);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<PaginatedList<Quest>>> SearchQuestByDescription(string? firstLetter, string? nameContains, int? pageNumber = 1, int pageSize = 10)
        {
            var quests = _context.Quest.AsQueryable();

            // Appliquer le filtre pour le nom qui commence par la première lettre spécifique
            if (!string.IsNullOrEmpty(firstLetter))
            {
                quests = quests.Where(i => i.Description.StartsWith(firstLetter));
            }

            if (!string.IsNullOrEmpty(nameContains))
            {
                quests = quests.Where(i => EF.Functions.Like(i.Description, $"%{nameContains}%"));
            }


            // Calculer le nombre total avant la pagination
            var totalCount = await quests.CountAsync();

            // Utiliser PaginatedList pour créer une liste paginée
            var pagedQuests = await PaginatedList<Quest>.CreateAsync(quests.AsNoTracking(), pageNumber ?? 1, pageSize);

            if (totalCount == 0)
            {
                return NotFound("Aucune quêtes n'a été trouvé qui correspond à ces critères.");
            }
            return Ok(pagedQuests);
        }
        // PUT: api/Quest/Update/{id}
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Update(int id, [FromQuery] string? title = null, [FromQuery] string? description = null, [FromQuery] int? reward = null, [FromQuery] int? itemId = null)
        {
            Quest quest = await _context.Quest.FindAsync(id);
            if (quest == null)
            {
                return NotFound($"Quest with ID {id} not found.");
            }

            if (title != null)
            {
                quest.Title = title;
            }
            if (description != null)
            {
                quest.Description = description;
            }
            if (reward != null)
            {
                quest.Reward = (int)reward;
            }
            if (itemId != null)
            {
                quest.ItemId = itemId;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok(quest);
        }

        // POST: api/Quest/Create
        [HttpPost("[action]")]
        public async Task<ActionResult<Quest>> Create([FromBody] Quest quest)
        {
            _context.Quest.Add(quest);
            await _context.SaveChangesAsync();

            return Ok(quest);
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

            return Ok(quest);
        }
    }
}
