using api.Dtos.Comment;
using api.Models;

namespace api.Mappers
{
    public static class CommentMappers
    {
        public static CommentDto ToCommentDtoFromComment(this Comment commentModel)
        {
            return new CommentDto
            {
                Id = commentModel.Id,
                Content = commentModel.Content,
                CreatedAt = commentModel.CreatedAt,
                PostId = commentModel.PostId,
            };
        }

        public static Comment ToCommentFromCreateCommentDto(this CreateCommentDto commentDto, int postId)
        {
            return new Comment
            {
                Content = commentDto.Content,
                PostId = postId,
            };
        }
    }
}
