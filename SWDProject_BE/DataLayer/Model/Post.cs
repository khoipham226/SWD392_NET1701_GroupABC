using System;
using System.Collections.Generic;

namespace DataLayer.Model
{
    public partial class Post
    {
        public Post()
        {
            Exchangeds = new HashSet<Exchanged>();
            Reports = new HashSet<Report>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public bool Status { get; set; }

        public virtual Product Product { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Exchanged> Exchangeds { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
    }
}
