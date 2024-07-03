using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
	public interface IGroupRepository: IGenericRepository<Group>
	{
		 Group Add(Group group);
		 List<Group> FindAllByUserId(int userId);
	}
}
