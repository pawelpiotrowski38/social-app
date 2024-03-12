using Microsoft.AspNetCore.Mvc;
using api.Interfaces;
using api.Mappers;
using api.Dtos.Comment;
using api.Helpers;

namespace api.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentsRepository _commentsRepository;
        private readonly IPostsRepository _postsRepository;
        public CommentsController(ICommentsRepository commentsRepository, IPostsRepository postsRepository)
        {
            _commentsRepository = commentsRepository;
            _postsRepository = postsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            var comments = await _commentsRepository.GetAllAsync(query);

            var commentsDto = comments.Select(c => c.ToCommentDtoFromComment());

            return Ok(commentsDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var comment = await _commentsRepository.GetByIdAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment.ToCommentDtoFromComment());
        }

        [HttpPost("{postId}")]
        public async Task<IActionResult> Create([FromRoute] int postId, [FromBody] CreateCommentDto commentDto)
        {
            if (!await _postsRepository.PostExists(postId))
            {
                return BadRequest("Post does not exist");
            }

            var commentModel = commentDto.ToCommentFromCreateCommentDto(postId);
            await _commentsRepository.CreateAsync(commentModel);

            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id}, commentModel.ToCommentDtoFromComment());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentDto commentDto)
        {
            var commentModel = await _commentsRepository.UpdateAsync(id, commentDto);

            if (commentModel == null)
            {
                return NotFound();
            }

            return Ok(commentModel.ToCommentDtoFromComment());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var commentModel = await _commentsRepository.DeleteAsync(id);

            if (commentModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
