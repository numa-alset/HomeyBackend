using System.Collections.ObjectModel;

namespace HomeyBackend.Core.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public UserInfo User { get; set; }
        public string UserId { get; set; }
        public string IdReciever { get; set; }
        public ICollection<Message> Messages { get; set; }
        public DateTime LastUpdate { get; set; }
        public Chat() 
        { 
        Messages = new Collection<Message>();
        }
    }
}
