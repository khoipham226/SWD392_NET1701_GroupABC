using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
	public interface IMessageRepository: IGenericRepository<Message>
	{
		Message Add(Message message);
		object FindByPostId(int postId);
	}
}
