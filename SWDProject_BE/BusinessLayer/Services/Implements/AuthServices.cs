using BusinessLayer.RequestModels;
using BusinessLayer.ResponseModels;
using DataLayer.Model;
using DataLayer.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PayPalCheckoutSdk.Orders;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BusinessLayer.Services.Implements
{
	public class AuthServices : IAuthServices
	{
		private readonly IConfiguration _configuration;
		private readonly IUsersService _userService;
		private readonly IUnitOfWork _unitOfWork;

		public AuthServices(IConfiguration configuration, IUsersService userService, IUnitOfWork unitOfWork)
		{
			_configuration = configuration;
			_userService = userService;
			_unitOfWork = unitOfWork;
		}


		public async Task<BaseResponse<LoginResponseModel>> AuthenticateAsync(string username, string password)
		{
			// Implement authentication logic here
			// For demonstration purposes, let's assume the user is valid if username and password match
			var user = await _userService.GetUserByUsernameAsync(username);
			if (user != null && user.Password == password)
			{
				string token =  GenerateJwtToken(username, user.RoleId, user.Id);

				return new BaseResponse<LoginResponseModel>()
				{
					Code = 200,
					Message = "",
					Data = new LoginResponseModel()
					{

						Token = token,

						User = new UsersResponseModel()
						{
							Id = user.Id,
							UserName = username,
							Email = user.Email,
							Dob = user.Dob,
							Address = user.Address,
							PhoneNumber = user.PhoneNumber,
							RoleId = user.RoleId,
							ImgUrl = user.ImgUrl
						},


					}
				};
			}
			return new BaseResponse<LoginResponseModel>()
			{
				Code = 404,
				Message = "Username or Password incorrect",
				Data = null
			};
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

		public async Task<BaseResponse<TokenModel>> RegisterAsync(RegisterModel registerModel)

		{
			var existingUser = await _unitOfWork.Repository<User>().FindAsync(u => u.UserName == registerModel.Username || u.Email == registerModel.Email);

			if (existingUser != null)
			{
				return new BaseResponse<TokenModel>
				{
					Code = 409, // Conflict code
					Message = "Username or email already exists",
				};
			}
			var user = new User()
			{
				Address = registerModel.Address,
				RoleId = 2,
				UserName = registerModel.Username,
				Email = registerModel.Email,
				Password = registerModel.Password,
				Dob = registerModel.Dob,
				PhoneNumber = registerModel.PhoneNumber,
			};

			await _unitOfWork.Repository<User>().InsertAsync(user);
			await _unitOfWork.CommitAsync();

			// Generate JWT token
			string token = GenerateJwtToken(user.UserName, user.RoleId, user.Id);

			return new BaseResponse<TokenModel> { 
				Code = 201,
				Message = "Register sucessfully",
				Data = new TokenModel
				{
					Token = token
				}

			};
		}
	}
}
