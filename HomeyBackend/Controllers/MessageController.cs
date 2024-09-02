using HomeyBackend.Core.Models;
using HomeyBackend.Persistance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HomeyBackend.Controllers
{
    [Route("/message")]
    public class MessageController(HomeyBackendDbContext context) : Controller
    {
        private readonly HomeyBackendDbContext context = context;


        [Authorize]
        [HttpGet("/getmessages")]
        public async Task<IActionResult> GetMessages(int idChat)
        {
            string? userId = User.FindFirstValue("id");

            if (userId == null) { return BadRequest("user not authorized"); }

            var messages=await context.Messages.Include(m=>m.Chat).Where(e =>e.ChatId == idChat).ToListAsync();

                return Ok(messages);
            
        }
        
        [Authorize]
        [HttpDelete("/Deletemessage/{idMessage}")]
        public async Task<IActionResult> DeleteMessages(int idMessage, [FromBody]string text)
        {
            string? userId = User.FindFirstValue("id");

            if (userId == null) { return BadRequest("user not authorized"); }

            var message=await context.Messages.SingleOrDefaultAsync(e =>e.Id == idMessage);
            if (message == null) { return BadRequest("no message with this id"); }
            context.Messages.Remove(message);
            await context.SaveChangesAsync();
                return Ok();
        }
    }
}
