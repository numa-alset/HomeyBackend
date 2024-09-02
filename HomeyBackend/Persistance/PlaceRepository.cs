using HomeyBackend.Controllers.Resources;
using HomeyBackend.Core;
using HomeyBackend.Core.Models;
using HomeyBackend.Extentions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Extensions;
using System.Linq.Expressions;

namespace HomeyBackend.Persistance
{
    public class PlaceRepository(HomeyBackendDbContext context) : IPlaceRepository
    {
        private readonly HomeyBackendDbContext context = context;

        public async Task<Place> GetPlaceAsync(int id)
        {
            return await context.Places.Include(p => p.PlaceDetailBoolean)
                .Include(p => p.PlaceDetailNumberRooms)
                .Include(p => p.UserInfo)
                .Include(p => p.placeImages)
                .SingleOrDefaultAsync(p => p.Id == id)
                ;
        }
        public void Add(Place place)
        {
         
            context.Places.Add(place);
        }
        public void Remove(Place place)
        {
            context.Places.Remove(place);
        }
        public void Update(Place place)
        {
            context.Places.Update(place);
        }
        public async Task<QueryResult<Place>> GetPlacesAsync( PlaceQuery placeQuery)
        {
            var result = new QueryResult<Place>();
            var query =  context.Places.Include(p => p.PlaceDetailBoolean)
               .Include(p => p.PlaceDetailNumberRooms)
               .Include(p => p.UserInfo)
               .Include(p => p.placeImages)
               .AsQueryable();
               ;
              if (placeQuery.BoolTypes !=null )
              {
                query = query.Where(p => p.PlaceDetailBoolean.SolarPower ==placeQuery.BoolTypes!.SolarPower || placeQuery.BoolTypes.SolarPower == false);
                query = query.Where(p => p.PlaceDetailBoolean.Elevator == placeQuery.BoolTypes!.Elevator || placeQuery.BoolTypes.Elevator == false);
                query = query.Where(p => p.PlaceDetailBoolean.Wifi == placeQuery.BoolTypes!.Wifi || placeQuery.BoolTypes.Wifi == false);
                query = query.Where(p => p.PlaceDetailBoolean.Pool== placeQuery.BoolTypes!.Pool || placeQuery.BoolTypes.Pool == false);
                query = query.Where(p => p.PlaceDetailBoolean.Garden== placeQuery.BoolTypes!.Garden || placeQuery.BoolTypes.Garden == false);
                query = query.Where(p => p.PlaceDetailBoolean.Furntiure== placeQuery.BoolTypes!.Furntiure || placeQuery.BoolTypes.Furntiure  == false);
              }
              if (placeQuery.RoomTypes != null)
              {

                query = query.Where(p => p.PlaceDetailNumberRooms.NumberOfBaths == placeQuery.RoomTypes.NumberOfBaths|| placeQuery.RoomTypes.NumberOfBaths==0);
                query = query.Where(p => p.PlaceDetailNumberRooms.NumberOfBeds == placeQuery.RoomTypes.NumberOfBeds|| placeQuery.RoomTypes.NumberOfBeds==0);
                query = query.Where(p => p.PlaceDetailNumberRooms.NumberOfRooms == placeQuery.RoomTypes.NumberOfRooms|| placeQuery.RoomTypes.NumberOfRooms==0);
                query = query.Where(p => p.PlaceDetailNumberRooms.NumberOfSalons == placeQuery.RoomTypes.NumberOfSalons|| placeQuery.RoomTypes.NumberOfSalons==0);
              }

              var columnsMap = new Dictionary<SortBy, Expression<Func<Place, object>>>()
              {
                  [SortBy.Name] = p => p.Site,
                  [SortBy.Datetime] = v => v.CreateOn,

              };
              query = query.ApplyOrdering(placeQuery, columnsMap);

             query= query.Where(e=>e.Price>=placeQuery.MinPrice&&e.Price<=placeQuery.MaxPrice);
             if(placeQuery.Area>0)   
             query = query.Where(e=>e.Size>=(placeQuery.Area-10)&&e.Size<=(placeQuery.Area+10));
             if(placeQuery.PriceType!=null)
             query = query.Where(e=>e.PriceType==placeQuery.PriceType);
            
            result.TotalItems = await query.CountAsync();
            query = query.ApllyPaging(placeQuery);
            result.Items = await query.ToListAsync();
            return result;

        }
        public async Task<IEnumerable<Place>> GetPlacesallAsync()
        {
            return await context.Places.Include(p => p.PlaceDetailBoolean)
               .Include(p => p.PlaceDetailNumberRooms)
               .Include(p => p.UserInfo)
               .Include(p => p.placeImages)
               .ToListAsync();
        }
        public async Task<IEnumerable<Place>> SearchPlacesAsync(String name)
        {
            var query =  context.Places.Include(p => p.PlaceDetailBoolean)
               .Include(p => p.PlaceDetailNumberRooms)
               .Include(p => p.UserInfo)
               .Include(p => p.placeImages)
               .AsQueryable();
           query= query.Where(e=>e.Site.Contains(name));
            return await query.ToListAsync();
        }
    }
}
