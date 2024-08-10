using AutoMapper;
using HomeyBackend.Controllers.Resources;
using HomeyBackend.Core;
using HomeyBackend.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Xml.Linq;

namespace HomeyBackend.Controllers
{
    [Route("/api")]
    public class CommentController(ICommentRepository repository, IUnitOfWork unitOfWork,IMapper mapper) : Controller
    {
        private readonly ICommentRepository repository = repository;
        private readonly IUnitOfWork unitOfWork = unitOfWork;
        private readonly IMapper mapper = mapper;
        
        
    [HttpGet("/GetComments")]
    public async Task<IActionResult> GetComments(int placeId)
    {
            try
            {
                var comments = await repository.GetCommentsByPlaceAsync(placeId);
            
                var commentResource =  mapper.Map<IEnumerable<Comment>,IEnumerable<CommentResourceShow>>(comments);

                return Ok(commentResource);
            } catch(Exception ex) { return BadRequest(new { message = ex.Message }); }

    }
        [Authorize]
        [HttpDelete("/DeleteComment/{id}")]
        public async Task<IActionResult> DeletePlace(int id)
        {

            var userId = User.FindFirstValue("id");
            Console.WriteLine(userId);
            if (userId == null) { return Unauthorized("You must be logged in to create a place."); }
            try
            {
                repository.DeleteComment(id,userId);
                await unitOfWork.CompleteAsync();
                return Ok();
            } catch(Exception ex) { return BadRequest(new { message = ex.Message }); }
        }
        [Authorize]
        [HttpPut("/UpdateComment/")]
        public async Task<IActionResult> DeletePlace([FromForm] UpdateCommentResource update)
        {

            var userId = User.FindFirstValue("id");
            Console.WriteLine(userId);
            if (userId == null) { return Unauthorized("You must be logged in to create a place."); }
            try
            {
               var comment = await repository.UpdateCommentAsync(update,userId);
                await unitOfWork.CompleteAsync();
                return Ok(mapper.Map<Comment,CommentResourceShow>(comment));
            } catch(Exception ex) { return BadRequest(new { message = ex.Message }); }
        }
        [Authorize]
        [HttpPost("/AddComment/")]
        public async Task<IActionResult> AddPlace([FromForm] AddCommentResource add)
        {

            var userId = User.FindFirstValue("id");
            Console.WriteLine(userId);
            if (userId == null) { return Unauthorized("You must be logged in to create a place."); }
            try
            {
                repository.AddCommentAsync(add,userId);
                await unitOfWork.CompleteAsync();
                return Ok();
            } catch(Exception ex) { return BadRequest(new { message = ex.Message }); }
        }

    }
    
}
