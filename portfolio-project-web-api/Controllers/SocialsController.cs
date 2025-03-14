using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using portfolio_project_web_api.DAL.Context;
using portfolio_project_web_api.DAL.Entities;

namespace portfolio_project_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocialsController : ControllerBase
    {
        private readonly Context _context;

        
        public SocialsController(Context context)
        {
            _context = context;
        }


        [HttpGet("GetSocials")]
        public async Task<ActionResult<IEnumerable<SocialMedia>>> GetSocials()
        {
            var values = await _context.SocialMedias.ToListAsync();
            return Ok(values);
        }


        [HttpGet("GetSocialById/{id}")]
        public async Task<ActionResult<SocialMedia>> GetSocialById(int id)
        {
            var value = await _context.SocialMedias.FindAsync(id);
            if (value == null)
            {
                return NotFound();
            }
            return Ok(value);
        }


        [HttpPost("AddSocial")]
        public async Task<ActionResult> AddSocial([FromBody] SocialMedia social)
        {
            await _context.SocialMedias.AddAsync(social);
            await _context.SaveChangesAsync();
            return Ok();
        }


        [HttpPost("UpdateSocial")]
        public async Task<ActionResult> UpdateSocial(int id, [FromBody] SocialMedia social)
        {
            var value = await _context.SocialMedias.FindAsync(id);

            if (value == null)
            {
                return NotFound("Güncellenecek kayıt bulunamadı.");
            }

            value.Title = social.Title;
            value.Url = social.Url;

            await _context.SaveChangesAsync();
            return Ok("Güncelleme başarılı.");
        }


        [HttpPost("DeleteSocial/{id}")]
        public async Task<ActionResult> DeleteSocial(int id)
        {
            var about = await _context.SocialMedias.FindAsync(id);
            if (about == null)
            {
                return NotFound();
            }
            else
            {
                about.isActive = false;
                await _context.SaveChangesAsync();
                return Ok();
            }
        }


    }
}
