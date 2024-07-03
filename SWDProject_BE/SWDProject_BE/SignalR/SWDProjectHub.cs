
using DataLayer.Model;
using DataLayer.Repository;
using Microsoft.AspNetCore.SignalR;

namespace SWDProject_BE.SignalR
{
	public class SWDProjectHub: Hub
	{
		private readonly IGroupRepository _groupRepository;
		private readonly IMessageRepository _messageRepository;
		public SWDProjectHub(IGroupRepository groupRepository, IMessageRepository messageRepository)
		{
			_groupRepository = groupRepository;
			_messageRepository = messageRepository;
		}

		public override Task OnConnectedAsync()
		{
			return base.OnConnectedAsync();
		}

		public override Task OnDisconnectedAsync(Exception? exception)
		{
			return base.OnDisconnectedAsync(exception);
		}

		//tao nhom theo bai post
		public async Task CreateGroup(Group group)
		{
			
			//Save group to db
			var newGroup= _groupRepository.Add(group);
			//Goi lenh nhan group o Client
			//Gui cho tat ca client dang connect
			//Client se kiem tra minh co thuoc nhom vua tao khong
			//Neu thuoc nhom vua tao, se dc add vao nhom
			await Clients.All.SendAsync("ReceiveNewGroup", newGroup);				
		}

		//Join vao nhom
		public async Task JoinGroup(int postId)
		{
			//Dang ki nhan tin nhan tu Group voi groupId
			await Groups.AddToGroupAsync(Context.ConnectionId, postId.ToString());
			//
		}

		//Roi khoi nhom
		public async Task LeaveGroup(int postId)
		{
			//roi khoi nhom, khong nhan tin nhan nua
			await Groups.RemoveFromGroupAsync(Context.ConnectionId, postId.ToString());
		}

		//Join vao tat ca cac nhom, thuc hien khi bat dau ket noi(dang nhap)
		public async Task JoinAllGroup(int userId)
		{
			//Lay tat ca cac nhom ma user da tham gia
			var groups = _groupRepository.FindAllByUserId(userId);
			foreach (var group in groups)
			{
				await Groups.AddToGroupAsync(Context.ConnectionId, group.PostId.ToString());
			}
		}
		
		//Gui tin nhan
		public async Task SendMessage(Message message)
		{
			//Luu tin nhan vao db
			var newMessage = _messageRepository.Add(message);
			//Gui tin nhan theo group co PostId
			await Clients.Group(newMessage.PostId.ToString()).SendAsync("ReceiveMessage", newMessage);
		}

		//tai toan bo tin nhan tu nhom co ten la postId, thuc hien khi user mo form chat
		public async Task LoadMessageByPostId(int postId)
		{
			//lay tat ca tin nhan co postId
			//tai mot phan tin nhan, sau khi lan chuot se tiep tuc load
			var messages = _messageRepository.FindByPostId(postId);
			await Clients.Clients(Context.ConnectionId).SendAsync("ReceiveMessages", messages);
		}

		//front-end task:
		//khoi tao HubConnection
		//dang ki function, cac function nay se duoc server goi lai 
		//ket noi Hub signalR BE qua duong link
		//front-end can xu ly reconnect( xu ly ket noi lai khi gap su co mang)
	}
}
