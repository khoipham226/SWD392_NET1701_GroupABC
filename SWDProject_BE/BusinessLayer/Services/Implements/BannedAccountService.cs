using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DataLayer.UnitOfWork;

namespace BusinessLayer.Services.Implements
{
	public class BannedAccountServices : IBannedAccountService
	{
		private readonly IUnitOfWork _unitOfWork;

		public BannedAccountServices(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
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

				var bannedAccount = await _unitOfWork.Repository<BannedAccount>().FindAsync(ba => ba.UserId == id && ba.Status == true);
				if (bannedAccount != null)
				{
					bannedAccount.Status = false;
					bannedAccount.ModifiedDate = DateTime.Now;
					await _unitOfWork.Repository<BannedAccount>().Update(bannedAccount, bannedAccount.Id);
				}
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
					Subject = "Account Unbanned",
					Body = $"Dear {user.Email},\n\nYour account has been unbanned.\n\nYou can now access your account. If you have any questions, please contact support.\n\nBest regards,\nStudent Exchange Web"
				};
				mailMessage.To.Add(user.Email);

				await smtpClient.SendMailAsync(mailMessage);
			}
		}
	}
}
