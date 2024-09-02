using Microsoft.AspNetCore.Identity;
using System.Collections.ObjectModel;

namespace HomeyBackend.Core.Models
{
    public class UserInfo :IdentityUser
    {
        
        public required String UserPhoto { get; set; }

        public ICollection<Place> Places { get; set; }
        public ICollection<Chat> Chats { get; set; }


       public UserInfo()
        {
            Places = new Collection<Place>();
            Chats = new Collection<Chat>();
        }
    }
}
