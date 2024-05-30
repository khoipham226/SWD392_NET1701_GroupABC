using BusinessLayer.RequestModels;
using BusinessLayer.Services;
using BusinessLayer.Services.Implements;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SWDProject_BE.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LoginController : ControllerBase
	{
		private readonly IAuthServices _authService;

		public LoginController(IAuthServices authServices)
		{
			_authService = authServices;
		}

		[HttpPost("login")]
		public IActionResult Login(LoginModel model)
		{
			var token = _authService.AuthenticateAsync(model.Username, model.Password);
			if (token == null)
			{
				return Unauthorized();
			}

			return Ok(new { Token = token });
		}

		[HttpPost("register")]
		public IActionResult Register(RegisterModel model)
		{
			// Implement user registration logic here

			// Once the user is registered, generate JWT token
			var token = _authService.GenerateJwtToken(model.Username, model.RoleId);
			return Ok(new { Token = token });
		}
	}
}
