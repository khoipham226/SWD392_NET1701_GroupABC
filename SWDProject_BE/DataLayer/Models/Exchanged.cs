using System;
using System.Collections.Generic;

namespace DataLayer.Models;

public partial class Exchanged
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int PostId { get; set; }

    public int OrderId { get; set; }

    public string? Description { get; set; }

    public DateTime Date { get; set; }

    public bool Status { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Post Post { get; set; } = null!;

    public virtual ICollection<Rating> Ratings { get; } = new List<Rating>();

    public virtual User User { get; set; } = null!;
}
