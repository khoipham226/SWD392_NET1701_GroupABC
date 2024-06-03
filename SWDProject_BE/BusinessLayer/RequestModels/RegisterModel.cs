using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.RequestModels
{
	public class RegisterModel
	{
		public int UserId {  get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
		public DateTime Dob { get; set; }
		public string Address { get; set; }
		public string PhoneNumber { get; set; }
		public int RoleId { get; set; } = 2;
	}
}
