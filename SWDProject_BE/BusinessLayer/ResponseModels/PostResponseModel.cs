﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ResponseModels
{
    public class PostResponseModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public bool Status { get; set; }
        public UserResponse User { get; set; } = null!;
        public ProductResponse Product { get; set; } = null!;
        public string? ImageUrl { get; set; }
    }

    public class UserResponse
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string? ImgUrl { get; set; }
    }

    public class ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? UrlImg { get; set; }
    }
}

