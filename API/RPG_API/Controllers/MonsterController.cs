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
            return Ok(monster);
        }


        [HttpGet("SearchByName")]
        public async Task<ActionResult<IEnumerable<Monster>>> SearchByName(
           [FromQuery] string? NameContains,
           [FromQuery] string? NameStartsBy,
           [FromQuery] int pageNumber = 1,
           [FromQuery] int pageSize = 10)
        {
            // Vérifier si tous les paramètres de recherche sont nuls
            if (string.IsNullOrEmpty(NameContains) &&
                string.IsNullOrEmpty(NameStartsBy))
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

        [HttpGet("SearchByInfo")]
        public async Task<ActionResult<IEnumerable<Monster>>> SearchByInfo(
            [FromQuery] int? Category,
                [FromQuery] int? Difficulty,
                [FromQuery] int? MapId,
                [FromQuery] int pageNumber = 1,
                [FromQuery] int pageSize = 10)
        {
            if (!Category.HasValue && !Difficulty.HasValue && !MapId.HasValue)
            {
                return BadRequest(new { Message = "Aucun filtre de recherche fourni" });

            }
            var monsters = _context.Monster.AsQueryable();

            if (Category.HasValue)
            {
                monsters = monsters.Where(m => m.Category == (Category)Category.Value);
            }

            if (Difficulty.HasValue)
            {
                monsters = monsters.Where(m => m.Difficulty == (DifficultyMonster)Difficulty.Value);
            }

            if (MapId.HasValue)
            {
                monsters = monsters.Where(m => m.Map.Id == MapId.Value);
            }

            int totalCount = await monsters.CountAsync();

            var items = await monsters.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            if (!items.Any())
            {
                return NotFound(new { Message = "Aucun résultat pour cette recherche" });
            }

            var response = new
            {
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Items = items
            };
            return Ok(response);


        }

        [HttpGet("SearchByStats")]
        public async Task<ActionResult<IEnumerable<Monster>>> SearchByStats(
                [FromQuery] int? minDamage,
                [FromQuery] int? maxDamage,
                [FromQuery] int? minArmor,
                [FromQuery] int? maxArmor,
                [FromQuery] int? minHealth,
                [FromQuery] int? maxHealth,
                [FromQuery] int? minXpReward,
                [FromQuery] int? maxXpReward,

                [FromQuery] int pageNumber = 1,
                [FromQuery] int pageSize = 10)
        {

            // checi si tous filtres sont nuls 
            if (!minDamage.HasValue && !maxDamage.HasValue &&
                !minArmor.HasValue && !maxArmor.HasValue &&
                !minHealth.HasValue && !maxHealth.HasValue &&
                !minXpReward.HasValue && !maxXpReward.HasValue)
            {
                return BadRequest(new { Message = "Aucun filtre de recherche fourni" });
            }


            // Vérifier si un des filtres est incomplet (min ou max manquant)
            if ((minDamage.HasValue != maxDamage.HasValue) ||
                (minArmor.HasValue != maxArmor.HasValue) ||
                (minHealth.HasValue != maxHealth.HasValue) ||
                (minXpReward.HasValue != maxXpReward.HasValue))
            {
                return BadRequest(new { Message = "Un filtre est incomplet, Vos filtres doivent contenir un minimum ET maximum." });
            }

            var monsters = _context.Monster.AsQueryable();

            // Filtre pour les dégâts
            if (minDamage.HasValue && maxDamage.HasValue)
            {
                monsters = monsters.Where(m => m.Damage >= minDamage.Value && m.Damage <= maxDamage.Value);
            }

            // Filtre pour l'armure
            if (minArmor.HasValue && maxArmor.HasValue)
            {
                monsters = monsters.Where(m => m.Armor >= minArmor.Value && m.Armor <= maxArmor.Value);
            }

            // Filtre pour la santé
            if (minHealth.HasValue && maxHealth.HasValue)
            {
                monsters = monsters.Where(m => m.Health >= minHealth.Value && m.Health <= maxHealth.Value);
            }

            // Filtre pour la récompense d'XP
            if (minXpReward.HasValue && maxXpReward.HasValue)
            {
                monsters = monsters.Where(m => m.XpGiven >= minXpReward.Value && m.XpGiven <= maxXpReward.Value);
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
        public async Task<ActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("Le numéro et la taille de la page doivent être supérieurs à 0.");
            }

            // Récupération du nombre total de monstres
            int totalMonsters = await _context.Monster.CountAsync();

            // Calcul du nombre d'éléments à sauter
            int skip = (pageNumber - 1) * pageSize;

            // Récupération des monstres avec pagination
            List<Monster> monsters = await _context.Monster
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            // Si la liste est vide, retourne NotFound
            if (monsters == null || !monsters.Any())
            {
                return NotFound();
            }

            // Retourner un objet avec le nombre total de monstres et les données paginées
            var response = new
            {
                TotalMonsters = totalMonsters,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Monsters = monsters
            };

            return Ok(response);
        }


        // PUT: api/Monster/Update/{id}
        [HttpPut("[action]/{id}")]
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
        [HttpPost("[action]")]
        public async Task<ActionResult<Monster>> Create([FromBody] Monster monster)
        {
            _context.Monster.Add(monster);
            await _context.SaveChangesAsync();

            return Ok(monster);
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
