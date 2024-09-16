using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RPG_API.Data.Context;
using RPG_API.Models;

namespace RPG_API.Controllers
{
    public class CharacterController : Controller
    {
        private readonly APIContext _context;

        public CharacterController(APIContext context)
        {
            _context = context;
        }

        //GET: api/Character/Get/{id}
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<Character>> Get(int id)
        {
           var character = await _context.Character.FindAsync(id);

            if(character == null)
            {
                return NotFound();
            }
            return character;
        }

        //PUT: api/Character/Update/{id}
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Update(int id, Character character)
        {

            if(id != character.Id)
            {
                return BadRequest();
            }

            var newCharacter = await _context.Character.FindAsync(id);
            if(newCharacter == null)
            {
                return NotFound();
            }
            newCharacter.Name = character.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CharacterExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Character
        [HttpPost("[action]")]
        public async Task<ActionResult<Character>> Add([FromBody]Character character)
        {
            _context.Character.Add(character);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCharacter", new { id = character.Id }, character); //Attention ici, il faudrait peut-être retourner un DTO selon le user
        }

        //DELETE: api/Character/id
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var character = await _context.Character.FindAsync(id);
            if(character == null)
            {
                return NotFound();
            }

            _context.Character.Remove(character);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool CharacterExists(int id)
        {
            return _context.Character.Any(e => e.Id == id);
        }
    }
}
