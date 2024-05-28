using System;
using System.Collections.Generic;

namespace DataLayer.Models;

public partial class Post
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int TransactionTypeId { get; set; }

    public int ProductId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string? Img { get; set; }

    public double Price { get; set; }

    public DateTime Date { get; set; }

    public bool Status { get; set; }

    public virtual ICollection<Exchanged> Exchangeds { get; } = new List<Exchanged>();

    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    public virtual Product Product { get; set; } = null!;

    public virtual ICollection<Report> Reports { get; } = new List<Report>();

    public virtual TransactionType TransactionType { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
