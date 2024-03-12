using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Data;
using api.Mappers;
using api.Dtos.Post;
using api.Interfaces;
using api.Helpers;

namespace api.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IPostsRepository _postsRepository;
        public PostsController(AppDbContext context, IPostsRepository postsRepository)
        {
            _context = context;
            _postsRepository = postsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            var posts = await _postsRepository.GetAllAsync(query);

            var postsDto = posts.Select(p => p.ToPostDtoFromPost());

            return Ok(postsDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var post = await _postsRepository.GetByIdAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            return Ok(post.ToPostDtoFromPost());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePostDto postDto)
        {
            var postModel = postDto.ToPostFromCreatePostDto();
            await _postsRepository.CreateAsync(postModel);

            return CreatedAtAction(nameof(GetById), new { id = postModel.Id }, postModel.ToPostDtoFromPost());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePostDto postDto)
        {
            var postModel = await _postsRepository.UpdateAsync(id, postDto);

            if (postModel == null)
            {
                return NotFound();
            }

            return Ok(postModel.ToPostDtoFromPost());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var postModel = await _postsRepository.DeleteAsync(id);

            if (postModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
