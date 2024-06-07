using System;
using System.Collections.Generic;

namespace DataLayer.Model
{
    public partial class Order
    {
        public Order()
        {
            Disputes = new HashSet<Dispute>();
            Exchangeds = new HashSet<Exchanged>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public int? PaymentId { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
        public DateTime Date { get; set; }
        public bool Status { get; set; }

        public virtual Payment? Payment { get; set; }
        public virtual Post Post { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Dispute> Disputes { get; set; }
        public virtual ICollection<Exchanged> Exchangeds { get; set; }
    }
}
