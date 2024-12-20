﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RPG_API.Data.Context;
using RPG_API.Models;


namespace RPG_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly APIContext _context;

        public ClassController(APIContext context)
        {
            _context = context;
        }

        //GET: api/Character/Get/{id}
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<Class>> Get(int id)
        {
            var classCharacter = await _context.Class.FindAsync(id);

            if (classCharacter == null)
            {
                return NotFound();
            }
            return Ok(classCharacter);
        }
        //GET: api/Class/Get/{name}
        [HttpGet("[action]/{name}")]
        public async Task<ActionResult<Class>> GetByName(string name)
        {
            Class _class = await _context.Class.FirstOrDefaultAsync(i => i.Name == name);

            if (_class == null)
            {
                return NotFound();
            }
            return Ok(_class);
        }
        //GET: api/Class/GetAll
        [HttpGet("[action]")]
        public async Task<ActionResult<List<Class>>> GetAll()
        {
            List<Class> classCharacter = await _context.Class.ToListAsync();

            if (classCharacter == null || classCharacter.FirstOrDefault() == null)
            {
                return NotFound();
            }
            return Ok(classCharacter);
        }
        //GET: api/Class/SearchItemsByName
        [HttpGet("[action]")]
        public async Task<ActionResult<PaginatedList<Class>>> SearchItemsByName(string? firstLetter, string? nameContains, int? pageNumber = 1, int pageSize = 10)
        {
            var _class = _context.Class.AsQueryable();

            // Appliquer le filtre pour le nom qui commence par la première lettre spécifique
            if (!string.IsNullOrEmpty(firstLetter))
            {
                _class = _class.Where(i => i.Name.StartsWith(firstLetter));
            }

            if (!string.IsNullOrEmpty(nameContains))
            {
                _class = _class.Where(i => EF.Functions.Like(i.Name, $"%{nameContains}%"));
            }


            // Calculer le nombre total avant la pagination
            var totalCount = await _class.CountAsync();

            // Utiliser PaginatedList pour créer une liste paginée
            var pagedClass = await PaginatedList<Class>.CreateAsync(_class.AsNoTracking(), pageNumber ?? 1, pageSize);

            if (totalCount == 0)
            {
                return NotFound("Aucune classe n'a été trouvé qui correspond à ces critères.");
            }
            return Ok(pagedClass);
        }

        //PUT: api/Class/Update
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Update(int id, [FromQuery] string? name = null, [FromQuery] int? boostDefence = null, [FromQuery] int? boostAttack = null)
        {
            var newClass = await _context.Class.FindAsync(id);
            if (newClass == null)
            {
                return NotFound();
            }
            if(name != null)
            {
                newClass.Name = name;
            }
            if(boostDefence != null)
            {
                newClass.BoostDefence = (int)boostDefence;
            }
            if (boostAttack != null)
            {
                newClass.BoostAttack =(int)boostAttack;
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest();
            }

            return Ok();
        }

        // POST: api/Class
        [HttpPost("[action]")]
        public async Task<ActionResult<Class>> Create([FromBody] Class classCharacter)
        {
            _context.Class.Add(classCharacter);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest();
            }

            return Ok(classCharacter);
        }

        //DELETE: api/Class/id
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var classCharacter = await _context.Class.FindAsync(id);
            if (classCharacter == null)
            {
                return NotFound();
            }

            _context.Class.Remove(classCharacter);
            await _context.SaveChangesAsync();

            return Ok(classCharacter);
        }
        private bool ClassExists(int id)
        {
            return _context.Class.Any(e => e.Id == id);
        }
    }
}
