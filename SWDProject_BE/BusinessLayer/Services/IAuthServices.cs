using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
	public interface IAuthServices
	{
		Task<string> AuthenticateAsync(string username, string password);
		string GenerateJwtToken(string username, int roleId);
	}
}
