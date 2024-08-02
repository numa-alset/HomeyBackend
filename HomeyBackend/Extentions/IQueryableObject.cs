using HomeyBackend.Controllers.Resources;
using HomeyBackend.Core;
using HomeyBackend.Core.Models;

namespace HomeyBackend.Extentions
{
    public interface IQueryableObjects
    {
        SortBy? SortBy { get; set; }
        bool IsSortAscending { get; set; }
        int Page { get; set; }
        public PlaceDetailBooleanRecourse? BoolTypes { get; set; }
        public PlaceDetailRoomsRecourse? RoomTypes { get; set; }
        int MinPrice { get; set; }
        int MaxPrice { get; set; }
        int Area { get; set; }
        EPriceType? PriceType { get; set; }
    }
    public enum SortBy
    {
        Name,
        Datetime,
    }
}
