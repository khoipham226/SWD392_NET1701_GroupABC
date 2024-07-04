using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
	public interface IGroupService
	{
		Group Add(Group group);
		List<Group> FindAllByUserId(int userId);
	}
}
