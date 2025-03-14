using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using portfolio_project_web_api.DAL.Context;
using portfolio_project_web_api.DAL.Entities;

namespace portfolio_project_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly Context _context;

        
        public PortfolioController(Context context)
        {
            _context = context;
        }

        [HttpGet("GetPortfolios")]
        public async Task<ActionResult<IEnumerable<Portfolio>>> GetPortfolios()
        {
            var values = await _context.Portfolios.ToListAsync();
            return Ok(values);
        }


        [HttpGet("GetPortfolioById/{id}")]
        public async Task<ActionResult<Portfolio>> GetPortfolioById(int id)
        {

            var value = await _context.Portfolios.FindAsync(id);
            if (value == null)
            {
                return NotFound();
            }
            return Ok(value);
        }


        [HttpPost("AddPortfolio")]
        public async Task<ActionResult> AddPortfolio([FromBody] Portfolio portfolio)
        {
            await _context.Portfolios.AddAsync(portfolio);
            await _context.SaveChangesAsync();
            return Ok();
        }


        [HttpPost("UpdatePortfolio")]
        public async Task<ActionResult> UpdatePortfolio(int id, [FromBody] Portfolio portfolio)
        {
            var value = await _context.Portfolios.FindAsync(id);

            if (value == null)
            {
                return NotFound("Güncellenecek kayıt bulunamadı.");
            }

            value.Title = portfolio.Title;
            value.Description = portfolio.Description;
            value.Url = portfolio.Url;
            value.ImgUrl = portfolio.ImgUrl;

            await _context.SaveChangesAsync();
            return Ok("Güncelleme başarılı.");
        }


        [HttpPost("DeletePortfolio/{id}")]
        public async Task<ActionResult> DeletePortfolio(int id)
        {
            var value = await _context.Portfolios.FindAsync(id);
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