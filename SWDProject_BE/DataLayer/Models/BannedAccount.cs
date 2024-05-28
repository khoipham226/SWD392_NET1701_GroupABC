using System;
using System.Collections.Generic;

namespace DataLayer.Models;

public partial class BannedAccount
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Description { get; set; } = null!;

    public DateTime Date { get; set; }

    public bool Status { get; set; }

    public virtual User User { get; set; } = null!;
}
