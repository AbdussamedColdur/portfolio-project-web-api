using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using portfolio_project_web_api.DAL.Context;
using portfolio_project_web_api.DAL.Entities;

namespace portfolio_project_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExperinceController : ControllerBase
    {
        private readonly Context _context;


        public ExperinceController(Context context)
        {
            _context = context;
        }


        [HttpGet("GetExperinces")]
        public async Task<ActionResult<IEnumerable<Experience>>> GetExperinces()
        {
            var values = await _context.Experiences.Where(x => x.isActive == true).ToListAsync();
            return Ok(values);
        }


        [HttpGet("GetExperinceById/{id}")]
        public async Task<ActionResult<Experience>> GetExperinceById(int id)
        {
            var value = await _context.Experiences.FindAsync(id);
            if (value == null)
            {
                return NotFound();
            }
            return Ok(value);
        }


        [HttpPost("AddExperince")]
        public async Task<ActionResult> AddExperince([FromBody] Experience experince)
        {
            await _context.Experiences.AddAsync(experince);
            await _context.SaveChangesAsync();
            return Ok();
        }


        [HttpPost("UpdateExperince")]
        public async Task<ActionResult> UpdateExperince(int id, [FromBody] Experience experince)
        {
            var value = await _context.Experiences.FindAsync(id);

            if (value == null)
            {
                return NotFound("Güncellenecek kayıt bulunamadı.");
            }

            value.Title = experince.Title;
            value.Head = experince.Head;
            value.Description = experince.Description;
            value.Date = experince.Date;

            await _context.SaveChangesAsync();
            return Ok("Güncelleme başarılı.");
        }


        [HttpPost("DeleteExperince/{id}")]
        public async Task<ActionResult> DeleteExperince(int id)
        {
            var value = await _context.Experiences.FindAsync(id);
            if (value == null)
            {
                return NotFound();
            }
            else
            {
                value.isActive = false;
                await _context.SaveChangesAsync();
                return Ok();
            }
        }


    }
}
