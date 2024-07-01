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
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error. Please try again later.");
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
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error. Please try again later.");
            }
        }

        [HttpGet("getAllByUserId")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Post>>> GetPostsByUserId()
        {
            try
            {
                // Take the user id from JWT
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    return Unauthorized();
                }
                var userId = int.Parse(userIdClaim.Value);

                var posts = await _postService.GetAllPostsByUserIdAsync(userId);
                return Ok(posts);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error. Please try again later.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(int id)
        {
            try
            {
                // Take the user id from JWT
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    return Unauthorized();
                }
                var userId = int.Parse(userIdClaim.Value);

                var post = await _postService.GetPostDetailAsync(id, userId);
                if (post == null)
                {
                    return NotFound();
                }
                return Ok(post);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error. Please try again later.");
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> CreatePost([FromBody] PostRequestModel createPostRequest)
        {
            try
            {
                // Take the user id from JWT
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    return Unauthorized();
                }
                var userId = int.Parse(userIdClaim.Value);

                // Create a new post
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

                // Add the post using the post service
                await _postService.AddPostAsync(post);

                // Return the created post
                return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error. Please try again later.");
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
                    return NotFound();
                }

                // Take the user id from JWT
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    return Unauthorized();
                }
                var userId = int.Parse(userIdClaim.Value);

                // Ensure that only the owner or an admin
                if (existingPost.UserId != userId && !User.IsInRole("Admin"))
                {
                    return Forbid();
                }

                // Update the post properties
                existingPost.ProductId = (int)updatePostRequest.ProductId;
                existingPost.Title = updatePostRequest.Title;
                existingPost.Description = updatePostRequest.Description;
                existingPost.Date = DateTime.Now;
                existingPost.ImageUrl = updatePostRequest.ImageUrl;

                await _postService.UpdatePostAsync(existingPost);
                return StatusCode(StatusCodes.Status200OK, new { message = "Post updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error. Please try again later.");
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
                    return NotFound();
                }

                // Take the user id from JWT
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    return Unauthorized();
                }
                var userId = int.Parse(userIdClaim.Value);

                // Ensure that only the owner or an admin
                if (existingPost.UserId != userId && !User.IsInRole("Moderator"))
                {
                    return Forbid();
                }

                await _postService.DeletePostAsync(id);
                return StatusCode(StatusCodes.Status200OK, new { message = "Post deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error. Please try again later.");
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
                    return NotFound();
                }

                await _postService.UpdatePostStatusAsync(id, newStatus);
                return StatusCode(StatusCodes.Status200OK, new { message = "Post Publish status updated to " + newStatus });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error. Please try again later.");
            }
        }

    }
}
