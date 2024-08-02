using System.Collections.ObjectModel;

namespace HomeyBackend.Core.Models
{
    
    public class Place
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string Floor { get; set; }
        public required EType Type { get; set; }
        public UserInfo UserInfo { get; set; }
        public float Price { get; set; }  
        public EPriceType PriceType { get; set; }  
        public int Size { get; set;}    
        public  PlaceDetailNumberRooms PlaceDetailNumberRooms { get; set; }
        public  int PlaceDetailNumberRoomsId { get; set; }
        public  PlaceDetailBoolean PlaceDetailBoolean { get; set; }
        public  int PlaceDetailBooleanId { get; set; }
        public int Rate { get; set; }   
        public ICollection<PlaceImage> placeImages { get; set; }
        public DateTime CreateOn { get; set; }

        public Place()
        {
            placeImages = new Collection<PlaceImage>();
        }
    }

}
