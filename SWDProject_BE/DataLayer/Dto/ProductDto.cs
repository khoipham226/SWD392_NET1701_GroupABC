using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Dto
{
    public class ProductDto
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

        public ProductDto(int id, int userId, int categoryId, string name, string? description, string? urlImg, int stockQuantity, bool status) 
        {
           Id = id;
           UserId = userId;
           CategoryId = categoryId;
           Name = name;
           Description = description;
           UrlImg = urlImg;
           StockQuantity = stockQuantity;
           Status = status;

        }
    }
}
