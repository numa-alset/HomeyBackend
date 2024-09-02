using AutoMapper.Configuration.Annotations;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeyBackend.Core.Models
{

    public class Place
    {
        public int Id { get; set; }
        public required string Site { get; set; }
        public required string Description { get; set; }
        public required string Floor { get; set; }
        public required EType Type { get; set; }
        
        public  UserInfo UserInfo { get; set; }
        public  string UserInfoId { get; set; }
        public float Price { get; set; }
        public EPriceType PriceType { get; set; }
        public int Size { get; set; }
        public PlaceDetailNumberRooms PlaceDetailNumberRooms { get; set; }
        public int PlaceDetailNumberRoomsId { get; set; }
        public PlaceDetailBoolean PlaceDetailBoolean { get; set; }
        public int PlaceDetailBooleanId { get; set; }
        public int Rate { get; set; }
        public ICollection<PlaceImage> placeImages { get; set; }
        public DateTime CreateOn { get; set; }
        public RateToPlace RateToPlace { get; set; }
        public ICollection<Rate> Rates { get; set; }
        public ICollection<Comment> Comments { get; set; }

        public Place()
        {
            placeImages = new Collection<PlaceImage>();
            Rates = new Collection<Rate>();
            Comments = new Collection<Comment>();
        }
    }

}
