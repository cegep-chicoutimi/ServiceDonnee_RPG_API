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


        [HttpGet("Search")]
        public async Task<ActionResult<IEnumerable<Monster>>> GetMonsters(
      [FromQuery] string? NameContains,
      [FromQuery] string? NameStartsBy,
      [FromQuery] int? Category,
      [FromQuery] int? Difficulty,
      [FromQuery] int? MapId,
      [FromQuery] int pageNumber = 1,
      [FromQuery] int pageSize = 10)
        {
            // Vérifier si tous les paramètres de recherche sont nuls
            if (string.IsNullOrEmpty(NameContains) &&
                string.IsNullOrEmpty(NameStartsBy) &&
                !Category.HasValue &&
                !Difficulty.HasValue &&
                !MapId.HasValue)
            {
                return BadRequest(new { Message = "Aucun filtre de recherche fourni." });
            }

            var monsters = _context.Monster.AsQueryable();

            // Filtres pour le nom
            if (!string.IsNullOrEmpty(NameContains))
            {
                monsters = monsters.Where(m => EF.Functions.Like(m.Name, $"%{NameContains}%"));
            }

            if (!string.IsNullOrEmpty(NameStartsBy))
            {
                monsters = monsters.Where(m => m.Name.StartsWith(NameStartsBy));
            }

            // Filtre pour la catégorie
            if (Category.HasValue)
            {
                monsters = monsters.Where(m => m.Category == (Category)Category.Value);
            }

            // Filtre pour la difficulté
            if (Difficulty.HasValue)
            {
                monsters = monsters.Where(m => m.Difficulty == (DifficultyMonster)Difficulty.Value);
            }

            // Filtre pour l'ID de la carte
            if (MapId.HasValue)
            {
                monsters = monsters.Where(m => m.Map.Id == MapId.Value);
            }

            // Calculer le nombre total avant la pagination
            var totalCount = await monsters.CountAsync();

            // Appliquer la pagination
            var items = await monsters
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Vérifier si des résultats ont été trouvés
            if (!items.Any())
            {
                return NotFound(new { Message = "Aucun résultat pour cette recherche." });
            }

            // Créer l'objet de réponse
            var response = new
            {
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Items = items
            };

            return Ok(response);
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
