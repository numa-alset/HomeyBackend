using AutoMapper;
using HomeyBackend.Controllers.Resources;
using HomeyBackend.Core;
using HomeyBackend.Core.Models;
using HomeyBackend.Persistance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HomeyBackend.Controllers
{
    [Route("/Rate")]
    public class RateController(
        IMapper mapper,
        UserManager<UserInfo> userManager,
        HomeyBackendDbContext context
            ) :  Controller
    {
        private readonly IMapper mapper = mapper;
        private readonly UserManager<UserInfo> userManager = userManager;
        private readonly HomeyBackendDbContext context = context;

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddRate([FromBody] RateResource resource)
        {
            
            var place = await context.Places
                .Include(p=>p.Rates)
                .Include(p=>p.RateToPlace)
                .SingleOrDefaultAsync( p => p.Id == resource.PlaceId );
            if ( place == null ) { return NotFound("no place with this id"); }


            var userId = User.FindFirstValue("id");
            Console.WriteLine(userId);
            if (userId == null) {return Unauthorized("You must be logged in to create a place.");}


            var user2 = await context.Users.SingleOrDefaultAsync(u => u.Id == userId);
            if ( user2 == null ) { return Unauthorized("no user found for this id"); }


            var rateToPlace = await context.RateToPlaces.SingleOrDefaultAsync(p => p.PlaceId == place.Id);
            if ( rateToPlace == null ) {


               await context.RateToPlaces.AddAsync(new RateToPlace() { SumOfRates = 0, TotalNumberOfRates = 0, Place = place, PlaceId = place.Id });
                await context.SaveChangesAsync();

                rateToPlace = await context.RateToPlaces.SingleOrDefaultAsync(p => p.PlaceId == place.Id);
            }
            var rate = await context.Rates.SingleOrDefaultAsync(p => p.PlaceId==place.Id && p.UserId==userId);
            if (rate == null)
            {
                place.Rates.Add(new Rate { UserId =userId,UserRate=resource.UserRate,PlaceId=resource.PlaceId});
                rateToPlace.Increase(resource.UserRate);
            }
            else
            {

              
                rateToPlace.Decrease(rate.UserRate);
                rate.UserRate = resource.UserRate;
                rateToPlace.Increase(rate.UserRate);
                
            }
            place.Rate= (int)((double)rateToPlace.SumOfRates/(double)rateToPlace.TotalNumberOfRates);
            context.SaveChanges();
            return Ok(place.Rate);

        }


    }
}
