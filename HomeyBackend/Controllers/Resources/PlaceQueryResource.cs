using HomeyBackend.Core;
using HomeyBackend.Extentions;

namespace HomeyBackend.Controllers.Resources
{
    public class PlaceQueryResource
    {
        public SortBy? SortBy { get; set; }
        public bool IsSortAscending { get; set; } = false;
        public int Page { get; set; } = 0;
        public PlaceDetailBooleanRecourse? BoolTypes { get; set; }
        public PlaceDetailRoomsRecourse? RoomTypes { get; set; }
        public int MinPrice { get; set; } = 0;
        public int MaxPrice { get; set; } = 100000000;
        public int Area { get; set; } = 0;
        public EPriceType? PriceType { get; set; }
    }
}
