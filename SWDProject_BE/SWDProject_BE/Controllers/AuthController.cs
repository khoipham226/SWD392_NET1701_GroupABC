using BusinessLayer.RequestModels;
using BusinessLayer.Services;
using DataLayer.Model;
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
			var result = _authService.AuthenticateAsync(model.Username, model.Password).Result;

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
	}
}
