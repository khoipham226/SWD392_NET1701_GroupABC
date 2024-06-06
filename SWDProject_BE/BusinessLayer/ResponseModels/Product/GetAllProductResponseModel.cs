using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ResponseModels.Product
{
    public class GetAllProductResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string? Description { get; set; }
        public string? UrlImg { get; set; }
        public int StockQuantity { get; set; }

        public GetAllProductResponseModel(int id = 0, int userId = 0, string userName = "", int categoryId = 0, string categoryName = "", string name = "", string? description = null, string? urlImg = null, int stockQuantity = 0)
        {
            Id = id;
            UserId = userId;
            UserName = userName;
            CategoryId = categoryId;
            CategoryName = categoryName;
            Name = name;
            Description = description;
            UrlImg = urlImg;
            StockQuantity = stockQuantity;
        }
    }
}
