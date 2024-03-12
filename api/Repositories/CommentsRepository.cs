using Microsoft.EntityFrameworkCore;
using api.Data;
using api.Dtos.Comment;
using api.Interfaces;
using api.Models;
using api.Helpers;

namespace api.Repositories
{
    public class CommentsRepository : ICommentsRepository
    {
        private readonly AppDbContext _context;
        public CommentsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetAllAsync(QueryObject query)
        {
            var comments = _context.Comments.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                comments = query.SortBy.Equals("old")
                    ? comments.OrderBy(c => c.CreatedAt)
                    : comments.OrderByDescending(c => c.CreatedAt);
            }
            else
            {
                comments = comments.OrderByDescending(c => c.CreatedAt);
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await comments.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Comment> CreateAsync(Comment commentModel)
        {
            await _context.AddAsync(commentModel);
            await _context.SaveChangesAsync();

            return commentModel;
        }

        public async Task<Comment?> UpdateAsync(int id, UpdateCommentDto commentDto)
        {
            var existingComment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);

            if (existingComment == null)
            {
                return null;
            }

            existingComment.Content = commentDto.Content;

            await _context.SaveChangesAsync();

            return existingComment;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var existingComment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);

            if (existingComment == null)
            {
                return null;
            }

            _context.Comments.Remove(existingComment);
            await _context.SaveChangesAsync();

            return existingComment;
        }
    }
}
