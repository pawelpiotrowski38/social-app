using Microsoft.AspNetCore.Mvc;
using api.Data;
using api.Mappers;
using Microsoft.EntityFrameworkCore;
using api.Dtos.Post;
using api.Models;

namespace api.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public PostsController(AppDbContext context)
        {
            _context = context;   
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var posts = await _context.Posts.Include(p => p.Comments).ToListAsync();

            var postsDto = posts.Select(p => p.ToPostDtoFromPost());

            return Ok(postsDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var post = await _context.Posts.Include(p => p.Comments).FirstOrDefaultAsync(p => p.Id == id);

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
            await _context.Posts.AddAsync(postModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = postModel.Id }, postModel.ToPostDtoFromPost());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePostDto postDto)
        {
            var postModel = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);

            if (postModel == null)
            {
                return NotFound();
            }

            postModel.Content = postDto.Content;

            await _context.SaveChangesAsync();

            return Ok(postModel.ToPostDtoFromPost());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var postModel = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);

            if (postModel == null)
            {
                return NotFound();
            }

            _context.Posts.Remove(postModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
