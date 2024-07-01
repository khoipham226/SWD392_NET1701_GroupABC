using System;
using System.Collections.Generic;

namespace DataLayer.Model
{
    public partial class User
    {
        public User()
        {
            BannedAccounts = new HashSet<BannedAccount>();
            Comments = new HashSet<Comment>();
            Exchangeds = new HashSet<Exchanged>();
            Orders = new HashSet<Order>();
            Posts = new HashSet<Post>();
            Products = new HashSet<Product>();
            Ratings = new HashSet<Rating>();
            Reports = new HashSet<Report>();
            Tokens = new HashSet<Token>();
        }

        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime Dob { get; set; }
        public string Address { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public int RoleId { get; set; }
        public string Gender { get; set; } = null!;
        public string ImgUrl { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? RatingCount { get; set; }
        public bool Status { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<BannedAccount> BannedAccounts { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Exchanged> Exchangeds { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        public virtual ICollection<Token> Tokens { get; set; }
    }
}
