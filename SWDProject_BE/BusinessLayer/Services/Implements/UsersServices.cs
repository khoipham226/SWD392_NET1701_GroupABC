using DataLayer.Model;
using DataLayer.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Implements
{
	public class UsersServices : IUsersService
	{
		private readonly IUnitOfWork _unitOfWork;

		public UsersServices(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public IEnumerable<User> GetUsers()
		{
			return _unitOfWork.Repository<User>().GetAll();
		}

		public async Task<User> GetUserByIdAsync(int id)
		{
			return await _unitOfWork.Repository<User>().GetById(id);
		}

		public async Task CreateUserAsync(User user)
		{
			await _unitOfWork.Repository<User>().InsertAsync(user);
			await _unitOfWork.CommitAsync();
		}

		public async Task UpdateUserAsync(User user)
		{
			await _unitOfWork.Repository<User>().Update(user, user.Id);
			await _unitOfWork.CommitAsync();
		}

		public async Task DeleteUserAsync(int id)
		{
			var user = await _unitOfWork.Repository<User>().GetById(id);
			if (user != null)
			{
				_unitOfWork.Repository<User>().Delete(user);
				await _unitOfWork.CommitAsync();
			}
		}

		public async Task<bool> UserExistsAsync(int id)
		{
			var user = await _unitOfWork.Repository<User>().GetById(id);
			return user != null;
		}

		public async Task<User> GetUserByUsernameAsync(string username)
		{
			// Truy vấn người dùng từ cơ sở dữ liệu theo tên người dùng
			var user = await _unitOfWork.Repository<User>()
				.FindAsync(predicate: u => u.UserName == username);

			return user;
		}
	}

}
