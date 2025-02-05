using JsonPlaceholderAPI.Interfaces;
using JsonPlaceholderAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace JsonPlaceholderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceholderController : ControllerBase
    {
        private readonly IJsonPlaceholderService _jsonPlaceholderService;

        public PlaceholderController(IJsonPlaceholderService jsonPlaceholderService)
        {
            _jsonPlaceholderService = jsonPlaceholderService;
        }

        [HttpGet("posts")]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _jsonPlaceholderService.GetPostsAsync();
            return Ok(posts);
        }

        [HttpGet("posts/{id}")]
        public async Task<IActionResult> GetPostById(int id)
        {
            var post = await _jsonPlaceholderService.GetPostByIdAsync(id);
            if (post == null) return NotFound("Post não encontrado.");
            return Ok(post);
        }

        [HttpPost("posts")]
        public async Task<IActionResult> CreatePost([FromBody] Post post)
        {
            var createdPost = await _jsonPlaceholderService.CreatePostAsync(post);
            if (createdPost == null) return BadRequest("Erro ao criar o post.");
            return CreatedAtAction(nameof(GetPostById), new { id = createdPost.Id }, createdPost);
        }

        [HttpPut("posts/{id}")]
        public async Task<IActionResult> UpdatePost(int id, [FromBody] Post post)
        {
            var updatedPost = await _jsonPlaceholderService.UpdatePostAsync(id, post);
            if (updatedPost == null) return BadRequest("Erro ao atualizar o post.");
            return Ok(updatedPost);
        }

        [HttpDelete("posts/{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var deleted = await _jsonPlaceholderService.DeletePostAsync(id);
            if (!deleted) return BadRequest("Erro ao excluir o post.");
            return NoContent();
        }
    }
}
