using api.Dtos.Post;
using api.Models;

namespace api.Mappers
{
    public static class PostMappers
    {
        public static PostDto ToPostDtoFromPost(this Post postModel)
        {
            return new PostDto
            {
                Id = postModel.Id,
                Content = postModel.Content,
                CreatedAt = postModel.CreatedAt,
                Comments = postModel.Comments.Select(c => c.ToCommentDtoFromComment()).ToList(),
            };
        }

        public static Post ToPostFromCreatePostDto(this CreatePostDto postDto)
        {
            return new Post
            {
                Content = postDto.Content,
            };
        }
    }
}
