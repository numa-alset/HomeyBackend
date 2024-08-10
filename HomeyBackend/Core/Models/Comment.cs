namespace HomeyBackend.Core.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public String Text { get; set; }
        public String UserInfoId { get; set; }
        public DateTime CreateOn {  get; set; }
        public int PlaceId { get; set; }



    }
}
