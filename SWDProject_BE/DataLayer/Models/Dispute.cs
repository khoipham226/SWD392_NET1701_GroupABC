using System;
using System.Collections.Generic;

namespace DataLayer.Models;

public partial class Dispute
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int OrderId { get; set; }

    public string? Description { get; set; }

    public DateTime Date { get; set; }

    public bool Status { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
