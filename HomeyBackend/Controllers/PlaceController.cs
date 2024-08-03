using AutoMapper;
using HomeyBackend.Controllers.Resources;
using HomeyBackend.Core;
using HomeyBackend.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace HomeyBackend.Controllers
{
    [Route("/api")]
    public class PlaceController : Controller
    {
        private readonly IMapper mapper;
        private readonly IPlaceRepository repository;
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<UserInfo> userManager;
        private readonly IOptionsSnapshot<PhotoSettings> options;
        private readonly PhotoSettings photoSettings;

        public PlaceController(IMapper mapper,IPlaceRepository repository,IUnitOfWork unitOfWork,UserManager<UserInfo>userManager,IOptionsSnapshot<PhotoSettings>options )
        {
            this.mapper = mapper;
            this.repository = repository;
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
            this.photoSettings = options.Value;
        }

        
        [HttpGet("/GetPlace/{id}")]
        public async Task<IActionResult> GetPlace(int id)
        {
            var place = await repository.GetPlaceAsync(id);

            if (  place == null)
                return NotFound();
            var placeResource = mapper.Map<Place, ShowPlaceResource>(place);

            return Ok(placeResource);


        }
        
        [Authorize]   
        [HttpDelete("/DeletePlace/{id}")]
        public async Task<IActionResult> DeletePlace(int id)
        {
            var place = await repository.GetPlaceAsync(id);

            if (place == null)
                return NotFound();
            repository.Remove(place);
            await unitOfWork.CompleteAsync();
            return Ok(id);

        }

        
        [HttpPost("/CreatePlace")]
        [Authorize]
        public async Task<IActionResult>CreatePlace([FromForm] PlaceRecourse placeRecourse)
        {
            
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var place = mapper.Map<PlaceRecourse, Place>(placeRecourse);
            Console.WriteLine(await userManager.FindByIdAsync(User.FindFirstValue("Id")));
           if (await userManager.FindByIdAsync(User.FindFirstValue("Id")) == null)
                return Unauthorized("You must be logged in to create a place.");
            
            place.UserInfo = await userManager.FindByIdAsync(User.FindFirstValue("Id"));
            ;
            
            if (placeRecourse.PlaceImagesResourse == null) return BadRequest("Null File");
            int counterImager = 0;
            foreach (var file in placeRecourse.PlaceImagesResourse)
            {

                if (counterImager >= 5) { break; }
                if (file.Length == 0) return BadRequest("Empty File");
                if (file.Length > photoSettings.MaxBytes) return BadRequest("Maximum size file exeeded");
                if (!photoSettings.IsSupported(file.FileName)) return BadRequest("one or more of the files have invalid file type");

                var uploadesFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "photos/places");
                if (!Directory.Exists(uploadesFolderPath))
                {
                    Directory.CreateDirectory(uploadesFolderPath);
                }
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(uploadesFolderPath, fileName);
                Console.WriteLine(filePath);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                var photo = new PlaceImage { PhotoUrl= fileName };
                place.placeImages.Add(photo);
                counterImager++;
            }
            repository.Add(place);
            await unitOfWork.CompleteAsync();
            place = await repository.GetPlaceAsync(place.Id);
            var res = mapper.Map<Place,ShowPlaceResource>(place);
            return Ok(res);
        }

        [Authorize]
        [HttpPut("/UpdatePlace/{id}")]
        public async Task<IActionResult> UpdatePlace(int id,[FromForm] PlaceRecourse placeRecourse)
        {
           // if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var place = await repository.GetPlaceAsync(id);
            
            if (place == null)
                return NotFound();
            
              mapper.Map<PlaceRecourse, Place>(placeRecourse, place);
           if ( placeRecourse.PlaceImagesResourse.Count>0)
            {
                place.placeImages.Clear();
                int counterImager = 0;
                foreach (var file in placeRecourse.PlaceImagesResourse)
                {
                    
                    if (counterImager >= 5) { break; }
                    if (file.Length == 0) return BadRequest("Empty File");
                    if (file.Length > photoSettings.MaxBytes) return BadRequest("Maximum size file exeeded");
                    if (!photoSettings.IsSupported(file.FileName)) return BadRequest("one or more of the files have invalid file type");

                    var uploadesFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "photos/places");
                    if (!Directory.Exists(uploadesFolderPath))
                    {
                        Directory.CreateDirectory(uploadesFolderPath);
                    }
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(uploadesFolderPath, fileName);
                    Console.WriteLine(filePath);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    var photo = new PlaceImage { PhotoUrl = fileName };
                    place.placeImages.Add(photo);
                    counterImager++;
                }
            }
            repository.Update(place);
            await unitOfWork.CompleteAsync();
            place = await repository.GetPlaceAsync(place.Id);

            var res = mapper.Map<Place, ShowPlaceResource>(place);
            return Ok(res);
        }
        [HttpPost("/GetAllPlacesWithFilters")]
        public async Task<QueryResultResource<ShowPlaceResource>> GetVehicles([FromBody] PlaceQueryResource filterResource)
        {
            var filter = mapper.Map<PlaceQueryResource, PlaceQuery>(filterResource);
            var queryResult = await repository.GetPlacesAsync(filter);
            var x=  mapper.Map<QueryResult<Place>, QueryResultResource<ShowPlaceResource>>(queryResult);
            Console.WriteLine(x.TotalItems);
            return x;
        }
        
        [HttpGet("/GetAllPlacesNoFilters")]
        public async Task<IEnumerable< ShowPlaceResource>> GetVehiclesall()
        {
            
            var queryResult = await repository.GetPlacesallAsync();
            return mapper.Map<IEnumerable< Place>,IEnumerable< ShowPlaceResource>>(queryResult);
        }
        [HttpPost("/SearchByName")]
        public async Task<IEnumerable<ShowPlaceResource>> SearchByName(String name)
        {

            var queryResult = await repository.SearchPlacesAsync(name);
            return mapper.Map<IEnumerable<Place>, IEnumerable<ShowPlaceResource>>(queryResult);
        }

    }
}
