using BusinessLayer.RequestModels;
using BusinessLayer.Services;
using BusinessLayer.Services.Implements;
using DataLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace SWDProject_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangedController : ControllerBase
    {
        private readonly IExchangedService _exchangedService;
        private readonly IPostService _postService;

        public ExchangedController(IExchangedService exchangedService, IPostService postService)
        {
            _exchangedService = exchangedService;
            _postService = postService;
        }

        [HttpGet("GetAllFinishedForUser")]
        public async Task<ActionResult<Exchanged>> GetAllFinishedExchangedByUserId()
        {
            // Take the user id from JWT
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { message = "Check jwt token." });
            }
            var userId = int.Parse(userIdClaim.Value);

            var exchanged = await _exchangedService.GetAllFinishedExchangedByUserIdAsync(userId);
            return Ok(exchanged);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Exchanged>> GetExchangedById(int id)
        {
            var exchanged = await _exchangedService.GetExchangedByIdAsync(id);
            if (exchanged == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { message = "Exchanged id not found." });
            }
            return Ok(exchanged);
        }

        [HttpGet("GetAllPendingForCustomer")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Exchanged>>> GetAllPendingExchangedByUserIdForCustomer()
        {
            // Take the user id from JWT
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { message = "Check jwt token." });
            }
            var userId = int.Parse(userIdClaim.Value);
            var exchanged = await _exchangedService.GetAllPendingExchangedByUserIdForCustomerAsync(userId);
            return Ok(exchanged);
        }

        [HttpGet("GetAllPendingForPoster")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Exchanged>>> GetAllPendingExchangedByUserIdForPoster()
        {
            // Take the user id from JWT
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { message = "Check jwt token." });
            }
            var userId = int.Parse(userIdClaim.Value);

            var exchanged = await _exchangedService.GetAllPendingExchangedByUserIdForPosterAsync(userId);
            return Ok(exchanged);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddExchanged(ExchangedRequestModel exchangedRequest)
        {
            var post = await _postService.GetPostByIdAsync(exchangedRequest.PostId);

            if (post.ExchangedStatus == true)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { message = "Cannot add exchange because the post is aldready exchanged." });
            }

            // Take the user id from JWT
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { message = "Check jwt token." });
            }
            var userId = int.Parse(userIdClaim.Value);

            // Ensure that not the owner
            if (post.UserId == userId)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new { message = "This Post is your own post." });
            }

            var exchanged = new Exchanged
            {
                UserId = userId,
                PostId = exchangedRequest.PostId,
                Description = exchangedRequest.Description,
                Date = DateTime.Now,
                Status = false
            };
            await _exchangedService.AddExchangedAsync(exchanged);

            foreach (var productId in exchangedRequest.ProductIds)
            {
                var exchangedProduct = new ExchangedProduct
                {
                    ExchangeId = exchanged.Id,
                    ProductId = productId
                };
                await _exchangedService.AddExchangedProductAsync(exchangedProduct);
            }
            return StatusCode(StatusCodes.Status200OK, new { message = "Exchange created successfully." });
        }

        [HttpPut("accept/{id}")]
        [Authorize]
        public async Task<ActionResult> UpdateExchangedStatusAccept(int id)
        {
            var existingExchanged = await _exchangedService.GetExchangedByIdAsync(id);
            var OwnPost = await _postService.GetPostByIdAsync(existingExchanged.PostId);

            // Take the user id from JWT
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { message = "Check jwt token." });
            }
            var userId = int.Parse(userIdClaim.Value);

            // Ensure that only the owner of the post
            if (OwnPost.UserId != userId)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new { message = "This Post is not your own post." });
            }

            await _exchangedService.UpdateExchangedStatusAcceptAsync(id);
            return StatusCode(StatusCodes.Status200OK, new { message = "Exchange accpept successfully." });
        }

        [HttpDelete("deny/{id}")]
        [Authorize]
        public async Task<ActionResult> UpdateExchangedStatusDeny(int id)
        {
            var existingExchanged = await _exchangedService.GetExchangedByIdAsync(id);
            var OwnPost = await _postService.GetPostByIdAsync(existingExchanged.PostId);

            // Take the user id from JWT
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { message = "Check jwt token." });
            }
            var userId = int.Parse(userIdClaim.Value);

            // Ensure that only the owner of the post
            if (OwnPost.UserId != userId)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new { message = "This Post is not your own post." });
            }
            await _exchangedService.UpdateExchangedStatusDenyAsync(id);
            return StatusCode(StatusCodes.Status200OK, new { message = "Exchange deny successfully." });
        }

        [HttpDelete("cancel/{id}")]
        [Authorize]
        public async Task<ActionResult> CancelExchanged(int id)
        {
            var existingExchanged = await _exchangedService.GetExchangedByIdAsync(id);

            // Take the user id from JWT
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { message = "Check jwt token." });
            }
            var userId = int.Parse(userIdClaim.Value);

            // Ensure that only the owner of the exchanged
            if (existingExchanged.UserId != userId)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new { message = "This exchange is not your." });
            }
            await _exchangedService.UpdateExchangedStatusDenyAsync(id);
            return StatusCode(StatusCodes.Status200OK, new { message = "Exchange cancel successfully." });
        }
    }
}
