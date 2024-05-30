using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.RequestModels
{
    public class PostRequestModel
    {
        public int TransactionTypeId { get; set; }

        public int? ProductId { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public string? Img { get; set; }

        public double Price { get; set; }

        public DateTime Date { get; set; }

        public bool Status { get; set; }
    }
}
