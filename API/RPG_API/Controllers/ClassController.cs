using Microsoft.AspNetCore.Http;
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
            return classCharacter;
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
            return classCharacter;
        }

        //PUT: api/Class/Update/{id}
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Class classCharacter)
        {

            if (id != classCharacter.Id)
            {
                return BadRequest();
            }

            var newClass = await _context.Class.FindAsync(id);
            if (newClass == null)
            {
                return NotFound();
            }
            newClass.Name = classCharacter.Name;
           
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

        // POST: api/Class
        [HttpPost("[action]/{class}")]
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

            return Created(classCharacter.Id.ToString(), classCharacter);
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

            return Ok();
        }
        private bool ClassExists(int id)
        {
            return _context.Class.Any(e => e.Id == id);
        }
    }
}
