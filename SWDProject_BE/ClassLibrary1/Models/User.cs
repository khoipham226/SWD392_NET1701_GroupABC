using System;
using System.Collections.Generic;

namespace ClassLibrary1.Models;

public partial class User
{
    public int Id { get; set; }

    public string Field { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime Dob { get; set; }

    public string Address { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public int RoleId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? RatingCount { get; set; }

    public bool Status { get; set; }

    public virtual ICollection<BannedAccount> BannedAccounts { get; } = new List<BannedAccount>();

    public virtual ICollection<Dispute> Disputes { get; } = new List<Dispute>();

    public virtual ICollection<Exchanged> Exchangeds { get; } = new List<Exchanged>();

    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    public virtual ICollection<Post> Posts { get; } = new List<Post>();

    public virtual ICollection<Product> Products { get; } = new List<Product>();

    public virtual ICollection<Rating> Ratings { get; } = new List<Rating>();

    public virtual ICollection<Report> Reports { get; } = new List<Report>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<Token> Tokens { get; } = new List<Token>();
}
