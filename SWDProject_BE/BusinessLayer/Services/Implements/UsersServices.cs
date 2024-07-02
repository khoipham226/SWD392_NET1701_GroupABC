using DataLayer.Model;
using DataLayer.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;

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

        public async Task BanUser(int id, string description)
        {
            var user = await _unitOfWork.Repository<User>().GetById(id);
            if (user != null)
            {
				user.Status = false;
                await _unitOfWork.Repository<User>().Update(user, id);
                await _unitOfWork.CommitAsync();

                var bannedAccount = new BannedAccount
                {
                    UserId = user.Id,
                    Description = description,
                    Date = DateTime.Now,
                    Status = true,
                    
                };
                await _unitOfWork.Repository<BannedAccount>().InsertAsync(bannedAccount);
                await _unitOfWork.CommitAsync();

                // Send email notification to the user
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("studentexchangeweb@gmail.com", "fwpl wpkw zhqe peyh")
                };

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress("studentexchangeweb@gmail.com"),
                    Subject = "Account Banned",
                    Body = $"Dear {user.Email},\n\nYour account has been banned for the following reason:\n\n{description}\n\nIf you believe this is a mistake, please contact support.\n\nBest regards,\nStudent Exchange Web"
                };
                mailMessage.To.Add(user.Email);

                await smtpClient.SendMailAsync(mailMessage);
            }
        }

        public async Task UnBanUser(int id)
        {
            var user = await _unitOfWork.Repository<User>().GetById(id);
            if (user != null)
            {
                user.Status = true;
                await _unitOfWork.Repository<User>().Update(user, id);
                await _unitOfWork.CommitAsync();

                var bannedAccount = await _unitOfWork.Repository<BannedAccount>().FindAsync(ba => ba.UserId == id);
				if (bannedAccount != null)
				{
					await _unitOfWork.Repository<BannedAccount>().HardDelete(bannedAccount.Id);
				}

                // Send email notification to the user
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("studentexchangeweb@gmail.com", "fwpl wpkw zhqe peyh")
                };

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress("studentexchangeweb@gmail.com"),
                    Subject = "Account Unbanned",
                    Body = $"Dear {user.Email},\n\nYour account has been unbanned.\n\nYou can now access your account. If you have any questions, please contact support.\n\nBest regards,\nStudent Exchange Web"
                };
                mailMessage.To.Add(user.Email);

                await smtpClient.SendMailAsync(mailMessage);
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
				.GetAll()
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserName == username);

			return user;
		}

		public async Task<User> GetUserByEmailAsync(string email)
		{
			// Truy vấn người dùng từ cơ sở dữ liệu theo tên người dùng
			var user = await _unitOfWork.Repository<User>()
				.FindAsync(predicate: u => u.Email == email);

			return user;
		}
	}

}
