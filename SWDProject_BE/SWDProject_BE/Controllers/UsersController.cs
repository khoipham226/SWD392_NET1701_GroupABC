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
			var user = await _userService.GetUserByIdAsync(id);
			if (user == null)
			{
				return NotFound();
			}

			// Map properties from userModel to user entity
			user.Field = userModel.Field;
			user.UserName = userModel.UserName;
			user.Password = userModel.Password;
			user.Email = userModel.Email;
			user.Dob = userModel.Dob;
			user.Address = userModel.Address;
			user.PhoneNumber = userModel.PhoneNumber;
			user.RoleId = userModel.RoleId;
			user.Status = userModel.Status;
			user.ModifiedDate = DateTime.Now;

			try
			{
				await _userService.UpdateUserAsync(user);
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!await UserExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}


		// POST: api/Users
		[HttpPost]
		[Authorize]
		public async Task<ActionResult<User>> PostUser(UserCreateRequestModel userModel)
		{
			// Map properties from userModel to create a new user entity
			var user = new User
			{
				Field = userModel.Field,
				UserName = userModel.UserName,
				Password = userModel.Password,
				Email = userModel.Email,
				Dob = userModel.Dob,
				Address = userModel.Address,
				PhoneNumber = userModel.PhoneNumber,
				RoleId = userModel.RoleId,
				Status = userModel.Status,
				CreatedDate = DateTime.Now
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
