using BusinessLayer.RequestModels;
using BusinessLayer.ResponseModels;
using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
	public interface IAuthServices
	{
<<<<<<< HEAD
		Task<BaseResponse<LoginResponseModel>> AuthenticateAsync(string email, string password);
		string GenerateJwtToken(string username, int roleId, int userId);
=======
		Task<BaseResponse<LoginResponseModel>> AuthenticateAsync(string username, string password);
		string GenerateJwtToken(string username, string roleName, int userId);
>>>>>>> thuan1

		Task<BaseResponse<TokenModel>> RegisterAsync(RegisterModel user);
		Task<BaseResponse<TokenModel>> AdminGenAcc(RegisterModel registerModel);

		Task<BaseResponse> SendAccount(int userId);

		Task<BaseResponse> ForgotPassword(int userId);
	}
}
