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
        public async Task<ActionResult<IEnumerable<About>>> GetContacts()
        {
            var values = await _context.Contacts.Where(x => x.isActive == true).ToListAsync();
            return Ok(values);
        }
    }
}
