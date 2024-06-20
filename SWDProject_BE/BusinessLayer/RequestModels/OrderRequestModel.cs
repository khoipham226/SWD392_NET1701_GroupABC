using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.RequestModels
{
    public class OrderRequestModel
    {
        public int UserId { get; set; }
        public List<OrderDetailRequest> OrderDetails { get; set; }
        public double TotalPrice { get; set; }
    }

    public class OrderDetailRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
    }
}
