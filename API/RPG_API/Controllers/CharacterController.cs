using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RPG_API.Data.Context;
using RPG_API.Models;
using RPG_API.Models.Base;

namespace RPG_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
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

        //GET: api/Character/GetAll
        [HttpGet("[action]")]
        public async Task<ActionResult<List<Character>>> GetAll()
        {
            List<Character> characters = await _context.Character.ToListAsync();

            if (characters == null || characters.FirstOrDefault() == null)
            {
                return NotFound();
            }
            return characters;
        }

        //PUT: api/Character/Update/{id}
        [HttpPut("[action]/{id}&{character}")]
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
            //TODO all value changable

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest();
            }

            return NoContent();
        }

        //PUT :api/Character/UpdateInventaire/{id}&{itemid}
        [HttpPut("[action]/{id}&{itemid}")]
        public async Task<IActionResult> AddItemToInventory(int id, int itemid)
        {

          // find item 
            Item item = await _context.Item.FindAsync(itemid);
            if (item == null)
            { 
                return NotFound(); 
            }

          // find the character
            Character character = await _context.Character.FindAsync(id);
            if (character == null)
            {
                return NotFound();
            }
            // add item to character's inventory
            character.Inventory.Add(item);
          

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest();
            }

            return NoContent();
        }

        // POST: api/Character
        [HttpPost("[action]/{character}")]
        public async Task<ActionResult<Character>> Create([FromBody]Character character)
        {
            _context.Character.Add(character);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest();
            }

            return Created(character.Id.ToString(), character);
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

            return Ok();
        }


        //DELETE: api/Character/id/itemid
        [HttpDelete("[action]/{id}&{itemid}")]
        public async Task<IActionResult> DeleteItemFromInventory(int id, int itemid)
        {
            // find item 
            Item item = await _context.Item.FindAsync(itemid);
            if (item == null)
            {
                return NotFound();
            }

            // find the character
            Character character = await _context.Character.FindAsync(id);
            if (character == null)
            {
                return NotFound();
            }


            // remove item from character's inventory

            var itemToRemove = character.Inventory.FirstOrDefault(i => i.Equals(item));
            if (itemToRemove != null)
            {
                character.Inventory.Remove(itemToRemove);
                if ( character.Inventory.FirstOrDefault(i => i.Equals(item)) == null)
                {
                    // if this item cannot be found in the inventory, remove relation
                    item.Characters.Remove(character);
                }
            }
            else
            {
                return NotFound();
            }
           


            await _context.SaveChangesAsync();

            return Ok();
        }
        private bool CharacterExists(int id)
        {
            return _context.Character.Any(e => e.Id == id);
        }
    }
}
