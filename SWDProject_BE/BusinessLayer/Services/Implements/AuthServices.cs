using DataLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Implements
{
	public class AuthServices : IAuthServices
	{
		private readonly IConfiguration _configuration;
		private readonly IUsersService _userService;

		public AuthServices(IConfiguration configuration, IUsersService userService)
		{
			_configuration = configuration;
			_userService = userService;
		}

		
		public async Task<string> AuthenticateAsync(string username, string password)
		{
			// Implement authentication logic here
			// For demonstration purposes, let's assume the user is valid if username and password match
			var user = await _userService.GetUserByUsernameAsync(username);
			if (user != null && user.Password == password)
			{
				return GenerateJwtToken(username, user.RoleId, user.Id);
			}
			return null;
		}

		public string GenerateJwtToken(string username, int roleId, int userId)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[]
				{
					new Claim(ClaimTypes.Name, username),
					new Claim(ClaimTypes.Role, roleId.ToString()), // Thêm vai trò của người dùng vào token
					new Claim(ClaimTypes.NameIdentifier, userId.ToString())
				}),
				Expires = DateTime.UtcNow.AddHours(1),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}
	}
}
