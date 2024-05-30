using DataLayer.Dto.Product;
using DataLayer.Models;
using DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public interface IProductService
    {

        public List<Product> GetAllProducts();
        public String addProduct(AddProductDto dto);


    }
}
