using HomeyBackend.Core.Models;

namespace HomeyBackend.Core
{
    public interface IPlaceRepository
    {
        void Add(Place place);
        Task<Place> GetPlaceAsync(int id);
        Task<IEnumerable<Place>> GetPlacesallAsync();
        Task<QueryResult<Place>> GetPlacesAsync(PlaceQuery placeQuery);
        void Remove(Place place);
        Task<IEnumerable<Place>> SearchPlacesAsync(string name);
        void Update(Place place);
    }
}