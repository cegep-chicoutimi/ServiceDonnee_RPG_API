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
            var character = await _context.Character.Include(c => c.Inventory).Where(c => c.Id == id).FirstOrDefaultAsync();


            if (character == null)
            {
                return NotFound();
            }
            return Ok(character);
        }
        //GET: api/Character/Get/{name}
        [HttpGet("[action]/{name}")]
        public async Task<ActionResult<Character>> GetByName(string name)
        {
            Character character = await _context.Character.FirstOrDefaultAsync(i => i.Name == name);

            if (character == null)
            {
                return NotFound("No character with this name found.");
            }
            return Ok(character);
        }
        //GET: api/Character/Get/{name}
        [HttpGet("[action]/{class}")]
        public async Task<ActionResult<Character>> GetByClass(int classId)
        {
            Character character = await _context.Character.FirstOrDefaultAsync(i => i.ClassId == classId);

            if (character == null)
            {
                return NotFound("No character with this class found.");
            }
            return Ok(character);
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
            return Ok(characters);
        }
        //PUT: api/Character/Update/{id}
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Update(int id, [FromQuery] string? name = null, [FromQuery] int? xp = null, [FromQuery] int? damage = null, [FromQuery] int? armor = null, [FromQuery] int? lives = null, [FromQuery] Class? _class = null)
        {

            var newCharacter = await _context.Character.FindAsync(id);
            if (newCharacter == null)
            {
                return NotFound();
            }
            if (name != null)
            {
                newCharacter.Name = (string)name;
            }
            if (xp != null) 
            {
                newCharacter.Xp = (int)xp;
            }
            if (damage != null)
            {
                newCharacter.Damage = (int)damage;
            }
            if (armor != null)
            {
                newCharacter.Armor = (int)armor;
            }
            if (lives != null)
            {
                newCharacter.Lives = (int)lives;
            }
            if (_class != null)
            {
                newCharacter.Class = _class;
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
        [HttpPut("[action]/{id}&{xp}")]
        public async Task<IActionResult> UpdateXP(int id, int xp)
        {
            var character = await _context.Character.FindAsync(id);
            if (character == null)
            {
                return NotFound();
            }

            if (id != character.Id)
            {
                return BadRequest();
            }

            try
            {
                character.Xp = xp;
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest();
            }

            return Ok("Xp updated.");
        }
        [HttpPut("[action]/{id}&{damage}")]
        public async Task<IActionResult> UpdateDamage(int id, int damage)
        {
            var character = await _context.Character.FindAsync(id);
            if (character == null)
            {
                return NotFound();
            }

            if (id != character.Id)
            {
                return BadRequest();
            }

            try
            {
                character.Damage = damage;
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest();
            }

            return Ok("Damage updated.");
        }
        [HttpPut("[action]/{id}&{armor}")]
        public async Task<IActionResult> UpdateArmor(int id, int armor)
        {
            var character = await _context.Character.FindAsync(id);
            if (character == null)
            {
                return NotFound();
            }

            if (id != character.Id)
            {
                return BadRequest();
            }

            try
            {
                character.Armor = armor;
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest();
            }

            return Ok("Armor updated.");
        }
        [HttpPut("[action]/{id}&{lives}")]
        public async Task<IActionResult> UpdateLives(int id, int lives)
        {
            var character = await _context.Character.FindAsync(id);
            if (character == null)
            {
                return NotFound();
            }

            if (id != character.Id)
            {
                return BadRequest();
            }


            try
            {
                character.Lives = lives;
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest();
            }

            return Ok("lives updated.");
        }

        [HttpPut("[action]/{id}&{questid}")]
        public async Task<IActionResult> AddQuest(int id, int questid)
        {
            // find the character
            Character character = await _context.Character.FindAsync(id);
            if (character == null)
            {
                return NotFound();
            }
            // find quest 
            Quest quest = await _context.Quest.FindAsync(questid);
            if (quest == null) { return NotFound(); }

            // add quest to character's quests
            character.Quests.Add(quest);


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest();
            }

            return Ok("Quest Added.");
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
            if (character.Inventory == null)
            {
                character.Inventory = new List<Item>();
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

            return Ok("Item Added.");
        }

        // POST: api/Character
        [HttpPost("[action]")]
        public async Task<ActionResult<Character>> Create([FromBody] Character character)
        {
            _context.Character.Add(character);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest("Invalid data entered.");
            }

            return Ok(character);
        }

        //DELETE: api/Character/id
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var character = await _context.Character.FindAsync(id);
            if (character == null)
            {
                return NotFound("CHaracter not found.");
            }

            _context.Character.Remove(character);
            await _context.SaveChangesAsync();

            return Ok("Character deleted.");
        }

        //DELETE: api/Character/id/itemid
        [HttpDelete("[action]/{id}&{itemid}")]
        public async Task<IActionResult> DeleteItemFromInventory(int id, int itemid)
        {
            // find item 
            Item item = await _context.Item.FindAsync(itemid);
            if (item == null)
            {
                return NotFound("Item not found.");
            }

            // find the character
            //Character character = await _context.Character.FindAsync(id);
            Character character = await _context.Character
            .Include(c => c.Inventory)
            .FirstOrDefaultAsync(c => c.Id == id);

            if (character == null)
            {
                return NotFound("Item not found.");
            }
            if (character.Inventory == null)
            {
                character.Inventory = new List<Item>();
                return NotFound("Character's inventory is empty.");
            }
            // remove item from character's inventory

            var itemToRemove = character.Inventory.FirstOrDefault(i => i.Id == item.Id);
            if (itemToRemove != null)
            {
                character.Inventory.Remove(itemToRemove);
                item.Characters.Remove(character);
            }
            else
            {
                return NotFound("Item not found in character's inventory.");
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest();
            }

            return Ok("Item deleted.");
        }
        //DELETE: api/Character/id/questid
        [HttpDelete("[action]/{id}&{questid}")]
        public async Task<IActionResult> DeleteQuest(int id, int questid)
        {
            // find quest 
            Quest quest = await _context.Quest.FindAsync(questid);
            if (quest == null) { return NotFound("Quest not found."); }

            // find the character
            Character character = await _context.Character.FindAsync(id);
            if (character == null)
            {
                return NotFound("Quest not found.");
            }


            // remove quest from character's quests

            var questToRemove = character.Quests.FirstOrDefault(i => i.Equals(quest));
            if (questToRemove != null)
            {
                character.Quests.Remove(questToRemove);
                if (character.Quests.FirstOrDefault(i => i.Equals(quest)) == null)
                {
                    // if this quest cannot be found in the quests, remove relation
                    quest.Characters.Remove(character);
                }
            }
            else
            {
                return NotFound("Quest not found.");
            }



            await _context.SaveChangesAsync();

            return Ok("Quest deleted.");
        }
        private bool CharacterExists(int id)
        {
            return _context.Character.Any(e => e.Id == id);
        }
    }
}
