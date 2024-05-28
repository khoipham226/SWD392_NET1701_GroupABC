using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public interface IAuthService
    {
        Task<bool> Register(string username, string password, string email);
        Task<User> Login(string username, string password);
    }
}
