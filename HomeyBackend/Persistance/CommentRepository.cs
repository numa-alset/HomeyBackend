using HomeyBackend.Controllers.Resources;
using HomeyBackend.Core;
using HomeyBackend.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeyBackend.Persistance
{
    public class CommentRepository(
        HomeyBackendDbContext context

        ) : ICommentRepository
    {
        private readonly HomeyBackendDbContext context = context;

        public  void AddCommentAsync(AddCommentResource resource, String userId)
        {
            
           
            var user =  context.Users.SingleOrDefault(u => u.Id == userId) ?? throw new Exception("no user found for this id");

            
            context.Comments.Add(new Comment { Text=resource.Text,UserInfoId=user.Id,PlaceId=resource.PlaceId});
             

        }
        public   void DeleteComment(int id, String userId)
        {
         
            var user = context.Users.SingleOrDefault(u => u.Id == userId) ?? throw new Exception("no user found for this id");

            var comment =  context.Comments.SingleOrDefault(c => c.Id == id) ?? throw new Exception("no comment found for this id");

            if (comment.UserInfoId != userId) throw new Exception("this comment is not for this user");
            else
            {
               
           context.Comments.Remove(comment);
            }

        }
        public async Task<Comment> UpdateCommentAsync(UpdateCommentResource resource, String userId)
        {
            var user = await context.Users.SingleOrDefaultAsync(u => u.Id == userId) ?? throw new Exception("no user found for this id");

            var comment = await context.Comments.SingleOrDefaultAsync(c => c.Id == resource.IdComment) ?? throw new Exception("no comment found for this id");

            if (comment.UserInfoId != userId) throw new Exception("this comment is not for this user");
            comment.Text = resource.NewComment;
            comment.CreateOn = DateTime.Now;
            context.Comments.Update(comment);
            return comment;

        }
        public async Task<IEnumerable<Comment>> GetCommentsByPlaceAsync(int placeId)
        {
            var place = await context.Places
                .Include(p=>p.UserInfo)
                .Include(p => p.Comments).SingleOrDefaultAsync(p => p.Id == placeId) ?? throw new Exception("no place related to this placeId");

            return place.Comments.ToList();

        }



    }
}
