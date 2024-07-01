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
		Task<BaseResponse<LoginResponseModel>> AuthenticateAsync(string username, string password);
		string GenerateJwtToken(string username, string roleName, int userId);

		Task<BaseResponse<TokenModel>> RegisterAsync(RegisterModel user);
	}
}
