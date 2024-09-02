namespace HomeyBackend.Core.Models
{
    public class Message
    {
        public int Id { get; set; } 
        public string IdSender { get; set; }
        public string IdReceiver { get; set; }
        public Chat Chat { get; set; }
        public int ChatId { get; set; }
        public string Content { get; set; }
        public DateTime CreateOn { get; set; }


    }
}
