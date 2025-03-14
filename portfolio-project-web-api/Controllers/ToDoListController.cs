using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using portfolio_project_web_api.DAL.Context;
using portfolio_project_web_api.DAL.Entities;

namespace portfolio_project_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListController : ControllerBase
    {
        private readonly Context _context;

        
        public ToDoListController(Context context)
        {
            _context = context;
        }


        [HttpGet("GetToDoLists")]
        public async Task<ActionResult<IEnumerable<ToDoList>>> GetToDoLists()
        {
            var values = await _context.ToDoLists.ToListAsync();
            return Ok(values);
        }


        [HttpGet("GetToDoListById/{id}")]
        public async Task<ActionResult<ToDoList>> GetToDoListById(int id)
        {
            var value = await _context.ToDoLists.FindAsync(id);
            if (value == null)
            {
                return NotFound();
            }
            return Ok(value);
        }


        [HttpPost("AddToDoList")]
        public async Task<ActionResult> AddToDoList([FromBody] ToDoList toDoList)
        {
            await _context.ToDoLists.AddAsync(toDoList);
            await _context.SaveChangesAsync();
            return Ok();
        }


        [HttpPost("UpdateToDoList")]
        public async Task<ActionResult> UpdateToDoList(int id, [FromBody] ToDoList toDoList)
        {
            var value = await _context.ToDoLists.FindAsync(id);

            if (value == null)
            {
                return NotFound("Güncellenecek kayıt bulunamadı.");
            }

            value.Title = toDoList.Title;
            value.status = toDoList.status;
            value.Date = toDoList.Date;
            value.ImgUrl = toDoList.ImgUrl;

            await _context.SaveChangesAsync();
            return Ok("Güncelleme başarılı.");
        }


        [HttpPost("DeleteToDoList/{id}")]
        public ActionResult DeleteToDoList(int id)
        {
            var value = _context.ToDoLists.Find(id);
            if (value == null)
            {
                return NotFound("Silinecek kayıt bulunamadı.");
            }
            _context.ToDoLists.Remove(value);
            _context.SaveChanges();
            return Ok("Silme işlemi başarılı.");
        }


    }
}
