using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using portfolio_project_web_api.DAL.Context;
using portfolio_project_web_api.DAL.Entities;

namespace portfolio_project_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {

        private readonly Context _context;

        public ContactController(Context context)
        {
            _context = context;
        }

        [HttpGet("GetContacts")]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContacts()
        {
            var values = await _context.Contacts.Where(x => x.isActive == true).ToListAsync();
            return Ok(values);
        }

        [HttpGet("GetContactById/{id}")]
        public async Task<ActionResult<Contact>> GetContactById(int id)
        {
            var value = await _context.Contacts.FirstOrDefaultAsync(x => x.ContactId == id);
            return Ok(value);
        }

        [HttpPost("AddContact")]
        public async Task<ActionResult> AddContact([FromBody] Contact contact)
        {
            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("UpdateContact")]
        public async Task<ActionResult> UpdateContact([FromBody] Contact contact)
        {
            _context.Contacts.Update(contact);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("DeleteContact/{id}")]
        public async Task<ActionResult> DeleteContact(int id)
        {
            var value = await _context.Contacts.FirstOrDefaultAsync(x => x.ContactId == id);
            value.isActive = false;
            _context.Contacts.Update(value);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
