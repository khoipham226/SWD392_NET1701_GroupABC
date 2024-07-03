using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
	public class Group
	{
		public int Id { get; set; }
		public int PostId { get; set; }

		public List<int> GroupMembers { get; set; } = new List<int>();
	}
}
