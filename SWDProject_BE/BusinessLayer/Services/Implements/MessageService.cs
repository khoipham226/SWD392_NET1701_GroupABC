using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Implements
{
	public class MessageService : IMessageService
	{
		private List<Message> _messages;

		public MessageService()
		{
			_messages = new List<Message>();
		}

		// Hàm thêm một Message vào danh sách
		public Message Add(Message message)
		{
			// Giả sử Id được tạo tự động tăng
			message.Id = _messages.Count > 0 ? _messages.Max(m => m.Id) + 1 : 1;
			message.CreatedDate = DateTime.Now;
			_messages.Add(message);
			return message;
		}

		// Hàm tìm tất cả các Message theo GroupId
		public List<Message> FindByPostId(int postId)
		{
			return _messages.Where(m => m.GroupId == postId).ToList();
		}

	
	}
}
