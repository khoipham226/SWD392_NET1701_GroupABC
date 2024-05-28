using System;
using System.Collections.Generic;

namespace DataLayer.Models;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool Status { get; set; }

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
