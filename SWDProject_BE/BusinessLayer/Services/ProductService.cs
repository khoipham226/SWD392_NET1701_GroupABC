using DataLayer.Dto.Product;
using DataLayer.Models;
using DataLayer.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class ProductService : IProductService
    {
        private IGenericRepository<Product> productRepository;

        public ProductService(IGenericRepository<Product> productRepository)
        {
            this.productRepository = productRepository;
        }

        public string addProduct(AddProductDto dto)
        {
            try
            {
                Product product = new Product();
                product.UserId = dto.UserId;
                product.Name = dto.Name;
                product.Description = dto.Description;
                product.CategoryId = dto.CategoryId;
                product.UrlImg = dto.UrlImg;
                product.StockQuantity = dto.StockQuantity;
                product.Status = true;
                productRepository.Insert(product);
                return "add product successfull!";
            }
            catch (Exception ex)
            {
                return "add product unsuccessfull!";
            }

            

            
        }

        public List<Product> GetAllProducts()
        {
            List<Product> list = productRepository.GetAll().ToList();  
            
            return list;
        }

    }
}
