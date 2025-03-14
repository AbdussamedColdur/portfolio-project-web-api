using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using portfolio_project_web_api.DAL.Context;
using portfolio_project_web_api.DAL.Entities;

namespace portfolio_project_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly Context _context;


        public SkillController(Context context)
        {
            _context = context;
        }


        [HttpGet("GetSkills")]
        public async Task<ActionResult<IEnumerable<Skill>>> GetSkills()
        {
            var values = await _context.Skills.ToListAsync();
            return Ok(values);
        }


        [HttpGet("GetSkillById/{id}")]
        public async Task<ActionResult<Skill>> GetSkillById(int id)
        {
            var value = await _context.Skills.FindAsync(id);
            if (value == null)
            {
                return NotFound();
            }
            return Ok(value);
        }


        [HttpPost("AddSkill")]
        public async Task<ActionResult> AddSkill([FromBody] Skill skill)
        {
            await _context.Skills.AddAsync(skill);
            await _context.SaveChangesAsync();
            return Ok();
        }


        [HttpPost("UpdateSkill")]
        public async Task<ActionResult> UpdateSkill(int id, [FromBody] Skill skill)
        {
            var value = await _context.Skills.FindAsync(id);

            if (value == null)
            {
                return NotFound("Güncellenecek kayıt bulunamadı.");
            }

            value.Title = skill.Title;
            value.Value = skill.Value;

            await _context.SaveChangesAsync();
            return Ok("Güncelleme başarılı.");
        }


        [HttpPost("DeleteSkill/{id}")]
        public async Task<ActionResult> DeleteSkill(int id)
        {
            var value = await _context.Skills.FindAsync(id);
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
