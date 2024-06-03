using System;
using System.Collections.Generic;

namespace DataLayer.Model
{
    public partial class TransactionType
    {
        public TransactionType()
        {
            Posts = new HashSet<Post>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
