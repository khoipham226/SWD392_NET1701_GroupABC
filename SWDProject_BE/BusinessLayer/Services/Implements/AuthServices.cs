using BusinessLayer.RequestModels;
using BusinessLayer.ResponseModels;
using DataLayer.Model;
using DataLayer.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Security.Cryptography;

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


		public async Task<BaseResponse<LoginResponseModel>> AuthenticateAsync(string email, string password)
		{
			// Implement authentication logic here
			// For demonstration purposes, let's assume the user is valid if username and password match
			var user = await _userService.GetUserByEmailAsync(email);
            
            if (user != null && VerifyPassword(password, user.Password))
			{
                var userWithRole = await _userService.GetUserByUsernameAsync(user.UserName);
                string token =  GenerateJwtToken(user.UserName, userWithRole.Role.Name, user.Id);

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
							UserName = user.UserName,
							Email = user.Email,
							Dob = user.Dob,
							Address = user.Address,
							PhoneNumber = user.PhoneNumber,
							RoleId = user.RoleId,
							ImgUrl = user.ImgUrl,
							Gender = user.Gender
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

		public string GenerateJwtToken(string username, string roleName, int userId)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[]
				{
					new Claim(ClaimTypes.Name, username),
					new Claim(ClaimTypes.Role, roleName), // Thêm vai trò của người dùng vào token
					new Claim(ClaimTypes.NameIdentifier, userId.ToString())
				}),
				Expires = DateTime.UtcNow.AddHours(24),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}

		public async Task<BaseResponse<TokenModel>> RegisterAsync(RegisterModel registerModel)

		{
			var existingUser = await _unitOfWork.Repository<User>().FindAsync(u => u.Email == registerModel.Email);

			if (existingUser != null)
			{
				return new BaseResponse<TokenModel>
				{
					Code = 409, // Conflict code
					Message = "Email already exists",
				};
			}

			var user = new User()
			{
				Address = registerModel.Address,
				RoleId = 2,
				UserName = registerModel.Username,
				Email = registerModel.Email,
				Password = HashPassword(registerModel.Password),
				Dob = registerModel.Dob,
				PhoneNumber = registerModel.PhoneNumber,
				Gender = registerModel.Gender,
				ImgUrl = registerModel.ImgUrl,
			};

			await _unitOfWork.Repository<User>().InsertAsync(user);
			await _unitOfWork.CommitAsync();

            var userWithRole = await _userService.GetUserByUsernameAsync(user.UserName);
            // Generate JWT token
            string token = GenerateJwtToken(user.UserName, userWithRole.Role.Name, user.Id);

			return new BaseResponse<TokenModel> { 
				Code = 201,
				Message = "Register sucessfully",
				Data = new TokenModel
				{
					Token = token
				}

			};
		}

		public async Task<BaseResponse<TokenModel>> AdminGenAcc(RegisterModel registerModel)

		{
			var existingUser = await _unitOfWork.Repository<User>().FindAsync(u => u.Email == registerModel.Email);

			if (existingUser != null)
			{
				return new BaseResponse<TokenModel>
				{
					Code = 409, // Conflict code
					Message = "Email already exists",
				};
			}

			var providePassword = GeneratePassword();
			var user = new User()
			{
				Address = registerModel.Address,
				RoleId = registerModel.RoleId,
				UserName = registerModel.Username,
				Email = registerModel.Email,
				Password = HashPassword(providePassword),
				Dob = registerModel.Dob,
				PhoneNumber = registerModel.PhoneNumber,
			};

			await _unitOfWork.Repository<User>().InsertAsync(user);
			await _unitOfWork.CommitAsync();

            var userWithRole = await _userService.GetUserByUsernameAsync(user.UserName);
            // Generate JWT token
            string token = GenerateJwtToken(user.UserName, userWithRole.Role.Name, user.Id);

			return new BaseResponse<TokenModel> { 
				Code = 201,
				Message = "Register sucessfully",
				Data = new TokenModel
				{
					Token = token
				}

			};
		}

		public async Task<BaseResponse> SendAccount(int userId)
		{
			try
			{
				var user = await _userService.GetUserByIdAsync(userId);
				var providePassword = GeneratePassword();
				user.Password = HashPassword(providePassword);
				await _unitOfWork.Repository<User>().Update(user, user.Id);
				await _unitOfWork.CommitAsync();

				var smtpClient = new SmtpClient("smtp.gmail.com");
				smtpClient.Port = 587;
				smtpClient.EnableSsl = true;
				smtpClient.UseDefaultCredentials = false;
				smtpClient.Credentials = new NetworkCredential("studentexchangeweb@gmail.com", "fwpl wpkw zhqe peyh");

				MailMessage mailMessage = new MailMessage();
				mailMessage.From = new MailAddress("studentexchangeweb@gmail.com");
				mailMessage.To.Add(user.Email);
				mailMessage.Subject = "YOUR ENTRY ACCOUNT";
				mailMessage.Body = "Welcome to our Exchange web!!!\nEmail: " + user.Email + "\nPassword: " + providePassword + "\n\nThis is temporary password. Please change your password after logged in.";

				await smtpClient.SendMailAsync(mailMessage);

				return new BaseResponse
				{
					Code = 200,
					Message = "Send succeed."
				};
			}
			catch (Exception ex)
			{
				return new BaseResponse
				{
					Code = 400,
					Message = "An error occurred: " + ex.Message
				};
			}
		}

		public async Task<BaseResponse> ForgotPassword(int userId)
		{
			try
			{
				var user = await _userService.GetUserByIdAsync(userId);

				var smtpClient = new SmtpClient("smtp.gmail.com");
				smtpClient.Port = 587;
				smtpClient.EnableSsl = true;
				smtpClient.UseDefaultCredentials = false;
				smtpClient.Credentials = new NetworkCredential("studentexchangeweb@gmail.com", "fwpl wpkw zhqe peyh");

				MailMessage mailMessage = new MailMessage();
				mailMessage.From = new MailAddress("studentexchangeweb@gmail.com");
				mailMessage.To.Add(user.Email);
				mailMessage.Subject = "YOUR PASSWORD";
				mailMessage.Body = "Email: " + user.Email + "\nPassword: " + user.Password + "\n";

				await smtpClient.SendMailAsync(mailMessage);

				return new BaseResponse
				{
					Code = 200,
					Message = "Send succeed."
				};
			}
			catch (Exception ex)
			{
				return new BaseResponse
				{
					Code = 400,
					Message = "An error occurred: " + ex.Message
				};
			}
		}

		public string HashPassword(string password)
		{
			// TODO: Implement password hashing algorithm
			// Generate a random salt
			byte[] salt = new byte[16];
			using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
			{
				rng.GetBytes(salt);
			}

			// Hash the password and salt using PBKDF2
			var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
			byte[] hash = pbkdf2.GetBytes(20);

			// Combine the salt and hash into a single string
			byte[] hashBytes = new byte[36];
			Array.Copy(salt, 0, hashBytes, 0, 16);
			Array.Copy(hash, 0, hashBytes, 16, 20);
			string hashedPassword = Convert.ToBase64String(hashBytes);

			return hashedPassword;
		}

		public bool VerifyPassword(string password, string hashedPassword)
		{
			// TODO: Implement password verification algorithm
			// Extract the salt and hash from the hashed password string
			byte[] hashBytes = Convert.FromBase64String(hashedPassword);
			byte[] salt = new byte[16];
			Array.Copy(hashBytes, 0, salt, 0, 16);
			byte[] hash = new byte[20];
			Array.Copy(hashBytes, 16, hash, 0, 20);

			// Compute the hash of the password and salt using PBKDF2
			var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
			byte[] computedHash = pbkdf2.GetBytes(20);

			// Compare the computed hash with the stored hash
			for (int i = 0; i < 20; i++)
			{
				if (hash[i] != computedHash[i])
				{
					return false;
				}
			}
			return true;
		}

		public string GeneratePassword()
		{
			string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()";
			var bytes = new byte[8];
			using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
			{
				rng.GetBytes(bytes);
			}
			var password = new string(bytes.Select(b => characters[b % characters.Length]).ToArray());
			return password;
		}
	}
}
