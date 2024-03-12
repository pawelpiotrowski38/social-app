using api.Dtos.Post;
using api.Models;

namespace api.Interfaces
{
    public interface IPostsRepository
    {
        Task<List<Post>> GetAllAsync();
        Task<Post?> GetByIdAsync(int id);
        Task<Post> CreateAsync(Post postModel);
        Task<Post?> UpdateAsync(int id, UpdatePostDto postDto);
        Task<Post?> DeleteAsync(int id);
        Task<bool> PostExists(int id);
    }
}
