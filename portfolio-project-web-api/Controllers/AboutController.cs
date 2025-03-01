using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using portfolio_project_web_api.DAL.Context;
using portfolio_project_web_api.DAL.Entities;

[Route("api/About")]
[ApiController]
public class AboutController : ControllerBase
{
    private readonly Context _context;

    public AboutController(Context context)
    {
        _context = context;
    }

    [HttpGet("GetAbouts")]
    public async Task<ActionResult<IEnumerable<About>>> GetAbouts()
    {
        var abouts = await _context.Abouts.Where(x => x.isActive == true).ToListAsync();
        return Ok(abouts);
    }

    [HttpGet("GetAboutById/{id}")]
    public async Task<ActionResult<About>> GetAboutById(int id)
    {
        var about = await _context.Abouts.FindAsync(id);
        if (about == null)
        {
            return NotFound();
        }
        return Ok(about);
    }

    [HttpPost("AddAbout")]
    public async Task<ActionResult<About>> AddAbout(About about)
    {
        _context.Abouts.Add(about);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPost("DeleteAbout/{id}")]
    public async Task<ActionResult<About>> DeleteAbout(int id)
    {
        var about = await _context.Abouts.FindAsync(id);
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

    [HttpPost("UpdateAbout/{id}")]
    public async Task<ActionResult> UpdateAbout(int id, [FromBody] About about)
    {
        var value = await _context.Abouts.FindAsync(id);

        if (value == null)
        {
            return NotFound("Güncellenecek kayıt bulunamadı.");
        }

        value.Title = about.Title;
        value.SubDescription = about.SubDescription;
        value.Details = about.Details;

        await _context.SaveChangesAsync();
        return Ok("Güncelleme başarılı.");
    }

}
