using EFCoreCodefirst.Data;
using EFCoreCodefirst.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreCodefirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly DataContext _context;

        public PersonController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Person>>> GetAllPersons()
        {
            var addresses = await _context.persons.Include(o => o.Addresses).Include(o => o.EmailAddresses).ToListAsync();
            return Ok(addresses);
        }
           
        [HttpGet("{PersonId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Person>> GetPersonWithId([FromRoute] int PersonId)
        {
            var person = await this._context.persons.Include(o => o.Addresses).Include(o => o.EmailAddresses).FirstOrDefaultAsync(x => x.Id == PersonId);
            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Person>> CreatePerson([FromBody] Person person)
        {
            if (person == null)
            {
                return BadRequest("Person object is null");
            }

            this._context.persons.Add(person);
            var records = await this._context.SaveChangesAsync();
            if (records > 0)
            {
                return Created(string.Empty, (object)person);
            }

            return BadRequest("Person was not created");
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePerson([FromBody] Person person)
        {
            if (person == null)
            {
                return BadRequest("Person object is null");
            }
            this._context.Update(person);
            await this._context.SaveChangesAsync();
            return Ok(person);
        }


        /*
        * This method should have Authorization, the user should be Administrator  
        */
        [HttpDelete("{PersonId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int PersonId)
        {
            var itemToDelete = await this._context.persons.Include(o => o.Addresses).Include(o => o.EmailAddresses).FirstOrDefaultAsync(x => x.Id == PersonId);
            if (itemToDelete != null)
            {
                this._context.RemoveRange(itemToDelete);
                await this._context.SaveChangesAsync();
                return Ok(itemToDelete);
            }

            return NotFound();
        }
    }
}
