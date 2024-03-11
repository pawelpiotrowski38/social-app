using Microsoft.EntityFrameworkCore;
using api.Data;
using api.Interfaces;
using api.Models;
using api.Dtos.Post;

namespace api.Repositories
{
    public class PostsRepository : IPostsRepository
    {
        private readonly AppDbContext _context;
        public PostsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Post>> GetAllAsync()
        {
            return await _context.Posts.Include(p => p.Comments).ToListAsync();
        }

        public async Task<Post?> GetByIdAsync(int id)
        {
            return await _context.Posts.Include(p => p.Comments).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Post> CreateAsync(Post postModel)
        {
            await _context.Posts.AddAsync(postModel);
            await _context.SaveChangesAsync();

            return postModel;
        }

        public async Task<Post?> UpdateAsync(int id, UpdatePostDto postDto)
        {
            var existingPost = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
            
            if (existingPost == null) {
                return null;
            }

            existingPost.Content = postDto.Content;

            await _context.SaveChangesAsync();

            return existingPost;
        }

        public async Task<Post?> DeleteAsync(int id)
        {
            var existingPost = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);

            if (existingPost == null)
            {
                return null;
            }

            _context.Posts.Remove(existingPost);
            await _context.SaveChangesAsync();

            return existingPost;
        }
    }
}
