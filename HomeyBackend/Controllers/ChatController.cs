using HomeyBackend.Core;
using HomeyBackend.Core.Models;
using HomeyBackend.Persistance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Security.Claims;

namespace HomeyBackend.Controllers
{
    [Route("/chat")]
    public class ChatController(HomeyBackendDbContext context,UserManager<UserInfo> userManager) : Controller
    {
        private readonly HomeyBackendDbContext context = context;
        private readonly UserManager<UserInfo> userManager;



        [Authorize]
        [HttpPost("/addchat")]
        public async Task<IActionResult> AddChat(String idReceiver)
        {
            string? userId =  User.FindFirstValue("id");
            
            if (userId == null) { return BadRequest("user not authorized"); }
            
           var chats = context.Chats.Where(e => e.User.Id == userId);
            if (chats.Any(e=>e.IdReciever==idReceiver)) {
              var chat=  chats.FirstOrDefault(e => e.IdReciever == idReceiver);
                chat.LastUpdate = DateTime.Now;
                context.SaveChanges();
                return Ok();
            }
            else {
                var chat =  new  Chat { IdReciever = idReceiver, LastUpdate = DateTime.Now, UserId=userId};
                context.Chats.Add(chat);
              await context.SaveChangesAsync();
                return Ok(chat);
            }
            
        } 
        
        [Authorize]
        [HttpGet("/Chats")]
        public async Task<IActionResult> getChats()
        {
            string? userId =  User.FindFirstValue("id");
            if (userId == null) { return BadRequest("user not authorized"); }
            var chats = context.Chats.Where(e => e.User.Id == userId).ToList();
            
                return Ok(chats);
            
            
        }
    }
}
