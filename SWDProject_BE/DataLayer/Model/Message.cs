using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
	public class Message
	{
		public int Id { get; set; }
		public int PostId { get; set; }
		public string Content {  get; set; }= string.Empty;
	}
}
