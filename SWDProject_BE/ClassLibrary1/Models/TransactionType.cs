using System;
using System.Collections.Generic;

namespace ClassLibrary1.Models;

public partial class TransactionType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool Status { get; set; }

    public virtual ICollection<Post> Posts { get; } = new List<Post>();
}
