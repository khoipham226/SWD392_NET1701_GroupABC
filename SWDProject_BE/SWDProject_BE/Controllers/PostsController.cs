using BusinessLayer.RequestModels;
using BusinessLayer.Services;
using DataLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace SWDProject_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
        {
            var posts = await _postService.GetAllPostsAsync();
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> CreatePost([FromBody] PostRequestModel createPostRequest)
        {
            //Take the user id from jwt
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }
            var userId = int.Parse(userIdClaim.Value);

            var post = new Post
            {
                UserId = userId,
                TransactionTypeId = createPostRequest.TransactionTypeId,
                ProductId = createPostRequest.ProductId,
                Title = createPostRequest.Title,
                Description = createPostRequest.Description,
                Img = createPostRequest.Img,
                Price = createPostRequest.Price,
                Date = createPostRequest.Date,
                Status = createPostRequest.Status
            };
            await _postService.AddPostAsync(post);
            return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult> UpdatePost(int id, PostRequestModel updatePostRequest)
        {
            var existingPost = await _postService.GetPostByIdAsync(id);
            if (existingPost == null)
            {
                return NotFound();
            }

            // Update the post properties
            existingPost.TransactionTypeId = updatePostRequest.TransactionTypeId;
            existingPost.ProductId = updatePostRequest.ProductId;
            existingPost.Title = updatePostRequest.Title;
            existingPost.Description = updatePostRequest.Description;
            existingPost.Img = updatePostRequest.Img;
            existingPost.Price = updatePostRequest.Price;
            existingPost.Date = updatePostRequest.Date;
            existingPost.Status = updatePostRequest.Status;

            await _postService.UpdatePostAsync(existingPost);
            return Ok("Post updated successfully.");
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> DeletePost(int id)
        {
            await _postService.DeletePostAsync(id);
            return Ok("Post deleted successfully.");
        }
    }
}
