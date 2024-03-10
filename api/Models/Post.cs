namespace api.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
