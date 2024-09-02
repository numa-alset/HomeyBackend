
using HomeyBackend.Core.Models;
using HomeyBackend.Persistance;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace HomeyBackend.Hubs
{
    
    public class ChatHub(HomeyBackendDbContext context) : Hub
    {
        private readonly HomeyBackendDbContext context = context;

        /*public async Task SendPrivateMessage(string receiverUsername, string message)
            {
                // Implement logic to send private messages (consider authentication, data access, etc.)
                // Assuming authentication is in place:
               // var sender = Context.User.Identity.Site;
                //Console.WriteLine(sender);

                // ... logic to retrieve receiver information and handle message sending



                // ... logic to save message to database (if applicable)

                await Clients.All.SendAsync("ReceiveMessage","test");
            }*/
        public async Task SendMessage(string userIdReceiver,int chatId,string massage)
        {

             context.Messages.Add(new Message {ChatId =chatId,Content=massage,CreateOn=DateTime.Now,IdReceiver=userIdReceiver,IdSender=Context.UserIdentifier });
             context.SaveChanges();
            await Clients.User(userIdReceiver).SendAsync("ReceiveMessage", massage);
           // await Clients.All.SendAsync("ReceiveMessage", Context.UserIdentifier);
        }public async Task UpdateMessage(int idMessage,string massage,string userIdReceiver)
        {

            var message = await context.Messages.SingleOrDefaultAsync(e => e.Id == idMessage);
            if (message != null)
            {
                message.Content = massage;
                context.Messages.Update(message);
                context.SaveChanges();
                await Clients.User(userIdReceiver).SendAsync("UpdateMessage", massage);
            }
           // await Clients.All.SendAsync("ReceiveMessage", Context.UserIdentifier);
        }
    }
}
