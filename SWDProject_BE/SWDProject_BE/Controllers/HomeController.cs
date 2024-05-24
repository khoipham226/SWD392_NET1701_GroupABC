using Microsoft.AspNetCore.Mvc;
using DataLayer.Repository;
using DataLayer.Model;

namespace SWDProject_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserRepository _userRepository;

        public AuthController()
        {
            _userRepository = new UserRepository();
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserDTO userDto)
        {
            if (_userRepository.UsernameExists(userDto.Username))
            {
                return BadRequest("Username already exists.");
            }

            var user = new User
            {
                Username = userDto.Username,
                Password = userDto.Password,
                Email = userDto.Email
            };

            _userRepository.AddUser(user);
            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserView userDto)
        {
            var user = _userRepository.GetUser(userDto.Username, userDto.Password);
            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            // In a real app, generate a JWT or session token
            return Ok("Login successful.");
        }
    }
}
