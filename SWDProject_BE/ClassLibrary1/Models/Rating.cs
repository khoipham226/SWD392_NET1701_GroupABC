using System;
using System.Collections.Generic;

namespace ClassLibrary1.Models;

public partial class Rating
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int ExchangeId { get; set; }

    public int Score { get; set; }

    public string? Description { get; set; }

    public DateTime Date { get; set; }

    public bool Status { get; set; }

    public virtual Exchanged Exchange { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
