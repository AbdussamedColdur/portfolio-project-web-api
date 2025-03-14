using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using portfolio_project_web_api.DAL.Context;
using portfolio_project_web_api.DAL.Entities;

namespace portfolio_project_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeatureController : ControllerBase
    {
        private readonly Context _context;


        public FeatureController(Context context)
        {
            _context = context;
        }


        [HttpGet("GetFeatures")]
        public async Task<ActionResult<IEnumerable<Feature>>> GetFeatures()
        {
            var values = await _context.Features.Where(x => x.isActive == true).ToListAsync();
            return Ok(values);
        }


        [HttpGet("GetFeatureById/{id}")]
        public async Task<ActionResult<Feature>> GetFeatureById(int id)
        {
            var value = await _context.Features.FindAsync(id);
            if (value == null)
            {
                return NotFound();
            }
            return Ok(value);
        }


        [HttpPost("AddFeature")]
        public async Task<ActionResult> AddFeature([FromBody] Feature feature)
        {
            await _context.Features.AddAsync(feature);
            await _context.SaveChangesAsync();
            return Ok();
        }


        [HttpPost("UpdateFeature")]
        public async Task<ActionResult> UpdateFeature(int id, [FromBody] Feature feature)
        {
            var value = await _context.Features.FindAsync(id);

            if (value == null)
            {
                return NotFound("Güncellenecek kayıt bulunamadı.");
            }

            value.Title = feature.Title;
            value.Description = feature.Description;

            await _context.SaveChangesAsync();
            return Ok("Güncelleme başarılı.");
        }


        [HttpPost("DeleteFeature/{id}")]
        public async Task<ActionResult> DeleteFeature(int id)
        {
            var value = await _context.Features.FindAsync(id);
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
