using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Implements
{
	public class GroupService : IGroupService
	{
		private List<Group> _groups;
		private List<User> _users;

		public GroupService()
		{
			_groups = new List<Group>();
			_users= new List<User>();
		}

		// Hàm thêm một Group vào danh sách
		public Group Add(Group group)
		{
			_groups.Add(group);
			return group;
		}

		// Hàm tìm tất cả các Group theo UserId
		public List<Group> FindAllByUserId(int userId)
		{
			var user = _users.FirstOrDefault(u => u.Id == userId);
			if (user == null)
			{
				return new List<Group>(); // Hoặc trả về null, tùy thuộc vào cách bạn muốn xử lý khi không tìm thấy User
			}
			return _groups.Where(g => g.UserId == userId).ToList();
		}
	}
}
