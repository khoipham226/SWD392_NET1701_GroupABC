using System;
using System.Collections.Generic;

namespace DataLayer.Models;

public partial class Order
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int PostId { get; set; }

    public int PaymentId { get; set; }

    public int Quantity { get; set; }

    public double TotalPrice { get; set; }

    public DateTime Date { get; set; }

    public bool Status { get; set; }

    public virtual ICollection<Dispute> Disputes { get; } = new List<Dispute>();

    public virtual ICollection<Exchanged> Exchangeds { get; } = new List<Exchanged>();

    public virtual Payment Payment { get; set; } = null!;

    public virtual Post Post { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
