using HomeyBackend.Controllers.Resources;
using HomeyBackend.Core.Models;

namespace HomeyBackend.Core
{
    public interface ICommentRepository
    {
        void AddCommentAsync(AddCommentResource resource, string userId);
        void DeleteComment(int id, string userId);
        Task<IEnumerable<Comment>> GetCommentsByPlaceAsync(int placeId);
        Task<Comment> UpdateCommentAsync(UpdateCommentResource resource, string userId);
    }
}