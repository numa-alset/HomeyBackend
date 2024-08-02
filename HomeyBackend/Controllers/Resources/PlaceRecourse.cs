using HomeyBackend.Core;
using HomeyBackend.Core.Models;
using System.Collections.ObjectModel;

namespace HomeyBackend.Controllers.Resources
{
    public class PlaceRecourse
    {
        public required string Name { get; set; } = "name";
        public required string Description { get; set; } = "name";
        public required string Floor { get; set; } = "name";
        public required EType Type { get; set; } = EType.Home;
        public float Price { get; set; } = 33.2f;
        public EPriceType PriceType { get; set; }
        public int Size { get; set; } = 33;
        public PlaceDetailRoomsRecourse PlaceDetailRoomsRecourse { get; set; }
        public PlaceDetailBooleanRecourse PlaceDetailBooleanRecourse { get; set; }
        public ICollection<IFormFile> PlaceImagesResourse { get; set; }

        public PlaceRecourse()
        {
            PlaceImagesResourse = new Collection<IFormFile>();
        }
    }
}
