using System;
using System.Collections.Generic;

namespace ClassLibrary1.Models;

public partial class Payment
{
    public int Id { get; set; }

    public string Date { get; set; } = null!;

    public double Amount { get; set; }

    public string Method { get; set; } = null!;

    public bool Status { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
