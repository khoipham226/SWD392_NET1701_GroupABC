using DataLayer.Model;
using DataLayer.Repository;
using DataLayer.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Register(string username, string password, string email)
        {
            var userRepo = _unitOfWork.Repository<User>();

            if (await userRepo.GetWhere(u => u.UserName == username).AnyAsync())
                return false;

            var user = new User
            {
                UserName = username,
                Password = /*HashPassword*/(password),
                Email = email,
                Address = "default Address",
                CreatedDate = DateTime.UtcNow,
                Field = "default Field",
                PhoneNumber = "0123456789",
                RoleId = 1,
                RatingCount = 0,
                Status = true,
            };

            await userRepo.InsertAsync(user);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<User> Login(string username, string password)
        {
            if (_unitOfWork == null)
            {
                throw new InvalidOperationException("UnitOfWork is not initialized.");
            }
            IGenericRepository<User> userRepo = _unitOfWork.Repository<User>();
            

            var user = await userRepo.FindAsync(u => u.UserName == username);
            
            if (user == null || !VerifyPassword(password, user.Password))
                return null;

            return user;
        }

        //private string HashPassword(string password)
        //{
        //    using var sha256 = System.Security.Cryptography.SHA256.Create();
        //    var bytes = Encoding.UTF8.GetBytes(password);
        //    var hash = sha256.ComputeHash(bytes);
        //    return Convert.ToBase64String(hash);
        //}

        private bool VerifyPassword(string password, string hashedPassword)
        {
            return password == hashedPassword;
        }
    }
}
