using api.Dtos.Comment;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface ICommentsRepository
    {
        Task<List<Comment>> GetAllAsync(QueryObject query);
        Task<Comment?> GetByIdAsync(int id);
        Task<Comment> CreateAsync(Comment commentModel);
        Task<Comment?> UpdateAsync(int id, UpdateCommentDto commentDto);
        Task<Comment?> DeleteAsync(int id);
    }
}
