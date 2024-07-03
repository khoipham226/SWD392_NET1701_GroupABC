using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataLayer.Model;
using BusinessLayer.Services;
using BusinessLayer.Services.Implements;
using BusinessLayer.RequestModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Drawing;

namespace SWDProject_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IUsersService _userService;

		public UsersController(IUsersService userService)
		{
			_userService = userService;
		}

		// GET: api/Users
		[HttpGet]
		[Authorize]
		public ActionResult<IEnumerable<User>> GetUsers()
		{
			var users = _userService.GetUsers();
			return Ok(users);
		}

		// GET: api/Users/5
		[HttpGet("{id}")]
		[Authorize]
		public async Task<ActionResult<User>> GetUser(int id)
		{
			var user = await _userService.GetUserByIdAsync(id);
			if (user == null)
			{
				return NotFound();
			}

			return Ok(user);
		}

		// PUT: api/Users/5
		[HttpPut("{id}")]
		[Authorize]
		public async Task<IActionResult> PutUser(int id, UserUpdateRequestModel userModel)
		{
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
				if (user == null)
				{
					return NotFound(new { message = "User ID not found." });
				}

				user.Dob = userModel.Dob;
				user.Address = userModel.Address;
				user.PhoneNumber = userModel.PhoneNumber;		
				user.ModifiedDate = DateTime.Now;
				user.ImgUrl = userModel.ImgUrl;
				user.Gender = userModel.Gender;
				user.UserName = userModel.UserName;

				await _userService.UpdateUserAsync(user);

                return Ok(new { message = "User updated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("BanUser/{id}")]
        [Authorize]
        public async Task<IActionResult> BanUser(int id, string reason)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                if (user == null)
                {
                    return NotFound(new { message = "User ID not found." });
                }
                if (user.Status == false)
                {
                    return BadRequest(new { message = "User Banned Already." });
                }

                await _userService.BanUser(id, reason);

                return Ok(new { message = "User Banned successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UnBanUser/{id}")]
        [Authorize]
        public async Task<IActionResult> UnBanUser(int id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                if (user == null)
                {
                    return NotFound(new { message = "User ID not found." });
                }
                if (user.Status == true)
                {
                    return BadRequest(new { message = "User UnBanned Already." });
                }

                await _userService.UnBanUser(id);

                return Ok(new { message = "User UnBanned successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // POST: api/Users
        [HttpPost]
		[Authorize]
		public async Task<ActionResult<User>> PostUser(UserCreateRequestModel userModel)
		{
			// Map properties from userModel to create a new user entity
			var user = new User
			{
				//Field = userModel.Field,
				UserName = userModel.UserName,
				Password = userModel.Password,
				Email = userModel.Email,
				Dob = userModel.Dob,
				Address = userModel.Address,
				PhoneNumber = userModel.PhoneNumber,
				RoleId = userModel.RoleId,
				Status = userModel.Status,
				CreatedDate = DateTime.Now,
				Gender = userModel.Gender,
				ImgUrl = userModel.ImgUrl,
			};

			await _userService.CreateUserAsync(user);
			return CreatedAtAction("GetUser", new { id = user.Id }, user);
		}

		// DELETE: api/Users/5
		[HttpDelete("{id}")]
		[Authorize]
		public async Task<IActionResult> DeleteUser(int id)
		{
			var user = await _userService.GetUserByIdAsync(id);
			if (user == null)
			{
				return NotFound();
			}

			await _userService.DeleteUserAsync(id);
			return NoContent();
		}

		private async Task<bool> UserExists(int id)
		{
			return await _userService.UserExistsAsync(id);
		}
	}

}
