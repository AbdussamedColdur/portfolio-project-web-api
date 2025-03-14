using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using portfolio_project_web_api.DAL.Context;
using portfolio_project_web_api.DAL.Entities;

namespace portfolio_project_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly Context _context;


        public MessageController(Context context)
        {
            _context = context;
        }


        [HttpGet("GetMessages")]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessages()
        {
            var values = await _context.Messages.ToListAsync();
            return Ok(values);
        }


        [HttpGet("GetMessageById/{id}")]
        public async Task<ActionResult<Message>> GetMessageById(int id)
        {
            var value = await _context.Messages.FindAsync(id);
            if (value == null)
            {
                return NotFound();
            }
            return Ok(value);
        }


        [HttpPost("AddMessage")]
        public async Task<ActionResult> AddMessage([FromBody] Message message)
        {
            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();
            return Ok();
        }


        [HttpPost("SetMessageIsActiveFalse/{id}")]
        public async Task<ActionResult> SetMessageIsActiveFalse(int id)
        {
            var value = await _context.Features.FirstOrDefaultAsync(x => x.FeatureId == id);
            if (value == null) {
                return NotFound();
            }
            value.isActive = false;
            _context.Features.Update(value);
            await _context.SaveChangesAsync();
            return Ok();
        }


        [HttpPost("SetMessageIsActiveTrue/{id}")]
        public async Task<ActionResult> SetMessageIsActiveTrue(int id)
        {
            var value = await _context.Features.FirstOrDefaultAsync(x => x.FeatureId == id);
            if (value == null) {
                return NotFound();
            }
            value.isActive = true;
            _context.Features.Update(value);
            await _context.SaveChangesAsync();
            return Ok();
        }


    }
}
