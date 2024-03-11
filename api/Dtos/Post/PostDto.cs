using api.Dtos.Comment;

namespace api.Dtos.Post
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public List<CommentDto> Comments { get; set; } = new List<CommentDto>();
    }
}
