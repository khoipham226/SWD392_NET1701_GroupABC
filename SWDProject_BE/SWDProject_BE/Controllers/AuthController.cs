using BusinessLayer.RequestModels;
using BusinessLayer.Services;
using DataLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SWDProject_BE.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthServices _authService;

		public AuthController(IAuthServices authServices)
		{
			_authService = authServices;
		}

		[HttpPost("login")]
		public IActionResult Login(LoginModel model)
		{
			var result = _authService.AuthenticateAsync(model.Email, model.Password).Result;

			return StatusCode((int)result.Code, result);
		}

		[HttpPost("send-email")]
		public async Task<ActionResult> Gets(int userId)
		{
			var result = await _authService.SendAccount(userId);
			return StatusCode((int)result.Code, result);
		}

		[HttpGet("forgot-password")]
		public async Task<ActionResult> ForgotPassword(int userId)
		{
			var result = await _authService.ForgotPassword(userId);
			return StatusCode((int)result.Code, result);
		}

		[HttpPost("register")]
		public IActionResult Register(RegisterModel model)
		{
			// Implement user registration logic here

			// Once the user is registered, generate JWT token
			//return Ok(_authService.RegisterAsync(model).Result);
			var result = _authService.RegisterAsync(model).Result;
			return StatusCode((int) result.Code, result);
		}

		[HttpPost("admin-create-account")]
		public IActionResult AdminGenAcc(RegisterModel model)
		{
			// Implement user registration logic here

			// Once the user is registered, generate JWT token
			//return Ok(_authService.RegisterAsync(model).Result);
			var result = _authService.RegisterAsync(model).Result;
			return StatusCode((int) result.Code, result);
		}
	}
}
