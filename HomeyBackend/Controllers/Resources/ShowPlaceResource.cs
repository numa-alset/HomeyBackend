using HomeyBackend.Core;
using HomeyBackend.Core.Models;
using System.Collections.ObjectModel;

namespace HomeyBackend.Controllers.Resources
{
    public class ShowPlaceResource
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string Floor { get; set; }
        public required string Type { get; set; }
        public String UserInfoId { get; set; }
        public float Price { get; set; }
        public String PriceType { get; set; }
        public int Size { get; set; }
        public PlaceDetailRoomsRecourse PlaceDetailRoomsRecourse{ get; set; }
        public PlaceDetailBooleanRecourse PlaceDetailBooleanRecourse { get; set; }
        public int Rate { get; set; }
        public ICollection<String> placeImagesResource { get; set; }
        public DateTime CreateOn { get; set; }

        public ShowPlaceResource()
        {
            placeImagesResource = new Collection<String>();
        }
    }
}
