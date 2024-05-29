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

        public List<Product> GetAllProducts()
        {
            List<Product> list = productRepository.GetAll().ToList();  
            
            return list;
        }

    }
}
