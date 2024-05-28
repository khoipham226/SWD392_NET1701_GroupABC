using System;
using System.Collections.Generic;

namespace DataLayer.Models;

public partial class Product
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? UrlImg { get; set; }

    public int StockQuantity { get; set; }

    public bool Status { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Post> Posts { get; } = new List<Post>();

    public virtual User User { get; set; } = null!;
}
