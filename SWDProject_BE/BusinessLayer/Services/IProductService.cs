using BusinessLayer.RequestModels.Product;
using DataLayer.Dto.Product;
using DataLayer.Model;
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

        List<Product> GetAllProducts();
        Task<String> addProduct(AddProductDto dto);

        Task<String> updateProduct(int id, UpdateProductDto dto);

        Task<String> deleteProduct(int id);

    }
}
