﻿using BusinessLayer.RequestModels;
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
            try
            {
                var posts = await _postService.GetAllValidPostsAsync();
                return Ok(posts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getAllPendingPost")]
        public async Task<ActionResult<IEnumerable<Post>>> GetAllUnpublicPosts()
        {
            try
            {
                var posts = await _postService.GetAllUnpublicPostsAsync();
                return Ok(posts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getAllByUserId")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Post>>> GetPostsByUserId()
        {
            try
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    return Unauthorized(new { message = "Check JWT token." });
                }
                var userId = int.Parse(userIdClaim.Value);

                var posts = await _postService.GetAllPostsByUserIdAsync(userId);
                return Ok(posts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(int id)
        {
            try
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    return Unauthorized(new { message = "Check JWT token." });
                }
                var userId = int.Parse(userIdClaim.Value);

                var post = await _postService.GetPostDetailAsync(id, userId);
                if (post == null)
                {
                    return NotFound(new { message = "Post ID not found." });
                }
                return Ok(post);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> CreatePost([FromBody] PostRequestModel createPostRequest)
        {
            try
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    return Unauthorized(new { message = "Check JWT token." });
                }
                var userId = int.Parse(userIdClaim.Value);

                var post = new Post
                {
                    UserId = userId,
                    ProductId = (int)createPostRequest.ProductId,
                    Title = createPostRequest.Title,
                    Description = createPostRequest.Description,
                    Date = DateTime.Now,
                    ImageUrl = createPostRequest.ImageUrl,
                    PublicStatus = false,
                    ExchangedStatus = false,
                };

                await _postService.AddPostAsync(post);

                return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult> UpdatePost(int id, PostRequestModel updatePostRequest)
        {
            try
            {
                var existingPost = await _postService.GetPostByIdAsync(id);
                if (existingPost == null)
                {
                    return NotFound(new { message = "Post ID not found." });
                }

                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    return Unauthorized(new { message = "Check JWT token." });
                }
                var userId = int.Parse(userIdClaim.Value);

                if (existingPost.UserId != userId && !User.IsInRole("Moderator"))
                {
                    return BadRequest("Only the PostOwner (or Moderator) can modify it");
                }

                existingPost.ProductId = (int)updatePostRequest.ProductId;
                existingPost.Title = updatePostRequest.Title;
                existingPost.Description = updatePostRequest.Description;
                existingPost.Date = DateTime.Now;
                existingPost.ImageUrl = updatePostRequest.ImageUrl;

                await _postService.UpdatePostAsync(existingPost);
                return Ok(new { message = "Post updated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> DeletePost(int id)
        {
            try
            {
                var existingPost = await _postService.GetPostByIdAsync(id);
                if (existingPost == null)
                {
                    return NotFound(new { message = "Post ID not found." });
                }

                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    return Unauthorized(new { message = "Check JWT token." });
                }
                var userId = int.Parse(userIdClaim.Value);

<<<<<<< HEAD
                // Ensure that only the owner or an admin
=======
>>>>>>> thuan1
                if (existingPost.UserId != userId && !User.IsInRole("Moderator"))
                {
                    return BadRequest("Only the PostOwner (or Moderator) can modify it");
                }

                await _postService.DeletePostAsync(id);
                return Ok(new { message = "Post deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateStatusPost/{id}")]
        [Authorize]
        public async Task<ActionResult> UpdateStatusPost(int id, Boolean newStatus)
        {
            try
            {
                var existingPost = await _postService.GetPostByIdAsync(id);
                if (existingPost == null)
                {
                    return NotFound(new { message = "Post ID not found." });
                }

                await _postService.UpdatePostStatusAsync(id, newStatus);
                return Ok(new { message = "Post publish status updated to " + newStatus });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
