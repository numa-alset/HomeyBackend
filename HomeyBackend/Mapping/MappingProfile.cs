using AutoMapper;
using AutoMapper.Features;
using HomeyBackend.Controllers.Resources;
using HomeyBackend.Core.Models;
using Microsoft.OpenApi.Extensions;
namespace HomeyBackend.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to API Rescourse
            // CreateMap<PlaceImage, PlaceImageResource>();
            CreateMap(typeof(QueryResult<>), typeof(QueryResultResource<>));
            CreateMap<PlaceDetailBoolean,PlaceDetailBooleanRecourse>();
            CreateMap<PlaceDetailNumberRooms,PlaceDetailRoomsRecourse>();
            CreateMap<Place, ShowPlaceResource>()
                .ForMember(pr => pr.UserInfoId, opt => opt.MapFrom(p => p.UserInfo.Id))
                .ForMember(pr=>pr.PlaceDetailBooleanRecourse,opt=>opt.MapFrom(p=>p.PlaceDetailBoolean))
                .ForMember(pr=>pr.PlaceDetailRoomsRecourse,opt=>opt.MapFrom(p=>p.PlaceDetailNumberRooms))
                .ForMember(pr=>pr.placeImagesResource,opt=>opt.MapFrom(p=>p.placeImages.Select(m=>m.PhotoUrl)))
                .ForMember(pr=>pr.PriceType,opt=>opt.MapFrom(p=>p.PriceType.GetDisplayName()))
                .ForMember(pr=>pr.Type,opt=>opt.MapFrom(p=>p.Type.GetDisplayName()))
                ;



            // Api Resource to Domain
           // CreateMap<PlaceImageResource, PlaceImage>();
            CreateMap<PlaceDetailBooleanRecourse, PlaceDetailBoolean>();
            CreateMap<PlaceQueryResource, PlaceQuery>();
            CreateMap<PlaceDetailRoomsRecourse, PlaceDetailNumberRooms>();
            CreateMap<PlaceRecourse,Place>()
                .ForMember(p=>p.Id,opt=>opt.Ignore())
                .ForMember(p=>p.UserInfo,opt=>opt.Ignore())
                .ForMember(p=>p.Rate,opt=>opt.Ignore())
                .ForMember(p=>p.placeImages,opt=>opt.Ignore())
                .ForMember(p=>p.CreateOn,opt=>opt.Ignore())
                
                //.ForMember(p=>p.placeImages,opt=> opt.MapFrom(v => v.PlaceImagesResourse.Select(vf => new PlaceImage{PhotoUrl=vf})))
                .AfterMap(
                (pr,p)=>p.PlaceDetailBoolean=new PlaceDetailBoolean {
                    Elevator=pr.PlaceDetailBooleanRecourse.Elevator,
                    Furntiure=pr.PlaceDetailBooleanRecourse.Furntiure,
                    Garden=pr.PlaceDetailBooleanRecourse.Garden,
                    Pool=pr.PlaceDetailBooleanRecourse.Pool,
                    SolarPower=pr.PlaceDetailBooleanRecourse.SolarPower,
                    Wifi=pr.PlaceDetailBooleanRecourse.Wifi,
                }
                ).AfterMap(
                (pr, p) => p.PlaceDetailNumberRooms = new PlaceDetailNumberRooms{
                    NumberOfBaths=pr.PlaceDetailRoomsRecourse.NumberOfBaths,
                    NumberOfBeds=pr.PlaceDetailRoomsRecourse.NumberOfBeds,
                    NumberOfRooms=pr.PlaceDetailRoomsRecourse.NumberOfRooms,
                    NumberOfSalons=pr.PlaceDetailRoomsRecourse.NumberOfSalons,
                
                }
                )
             /*.AfterMap(
                (pr, p) => { 
                var addrdImage= pr.PlaceImagesResourse.Select(m=>new PlaceImage {PhotoUrl=m.PhotoUrl}).ToList();
                    foreach (var f in addrdImage)
                        p.placeImages.Add(new PlaceImage { PhotoUrl =f.PhotoUrl,PlaceId=f.PlaceId});
                }
                )*/
                ;

        }
    }
}
