using DataLayer.Dto.Product;
using DataLayer.Model;
using DataLayer.Repository;
using Microsoft.EntityFrameworkCore;
using BusinessLayer.Services.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.UnitOfWork;
using BusinessLayer.RequestModels.Product;
using BusinessLayer.ResponseModels.Product;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BusinessLayer.Services
{
    public class ProductService : IProductService
    {
        private IUnitOfWork unitOfWork;
        private SWD392_DBContext context;
        public IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, SWD392_DBContext context, IMapper _mapper)
        {
            this.unitOfWork = unitOfWork;
            this.context = context;
            this._mapper = _mapper;
        }

        public async Task<String> addProduct(AddProductDto dto)
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
                await unitOfWork.Repository<Product>().InsertAsync(product);
                await unitOfWork.CommitAsync();
                return "add product successfull!";
            }
            catch (Exception ex)
            {
                throw;
            }   
        }

        public async Task<string> deleteProduct(int id)
        {
            try
            {
                var product = await unitOfWork.Repository<Product>().GetById(id);

                if (product != null)
                {
                    product.Status = false;    
                    await unitOfWork.Repository<Product>().Update(product, id);
                    await unitOfWork.CommitAsync();
                    return "Delete Successfull";
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<GetAllProductResponseModel>> GetAllProducts()
        {
            try
            {
                var Product = unitOfWork.Repository<Product>().FindAll(p => p.Status == true).ToList();
                List<GetAllProductResponseModel> Final = new List<GetAllProductResponseModel>();
                foreach (var product in Product)
                {
                    var user = await unitOfWork.Repository<User>().FindAsync(u => u.Id.Equals(product.UserId));
                    var category = await unitOfWork.Repository<Category>().FindAsync(c => c.Id.Equals(product.CategoryId));
                    var Subcategory = await unitOfWork.Repository<SubCategory>().FindAsync(c => c.Id.Equals(product.SubcategoryId));
                    GetAllProductResponseModel result = new GetAllProductResponseModel();
                    result = product.MapToGetAllProduct(_mapper);
                    result.UserName = user.UserName;
                    result.CategoryName = category.Name;
                    result.SubcategoryName = Subcategory.Name;
                    Final.Add(result);
                }
                return Final;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> updateProduct(int id,UpdateProductDto dto)
        {
            try
            {
                var product = await unitOfWork.Repository<Product>().GetById(id);
                if(product != null) 
                {
                    if (dto.UserId.HasValue)
                    {
                        product.UserId = dto.UserId.Value;
                    }
                    if (dto.CategoryId.HasValue)
                    {
                        product.CategoryId = dto.CategoryId.Value;
                    }
                    if (dto.Name != null)
                    {
                        product.Name = dto.Name;
                    }
                    if (dto.Price != 0)
                    {
                        product.Price = dto.Price;
                    }
                    if (dto.Description != null)
                    {
                        product.Description = dto.Description;
                    }
                    if (dto.UrlImg != null)
                    {
                        product.UrlImg = dto.UrlImg;
                    }
                    if (dto.StockQuantity.HasValue)
                    {
                        product.StockQuantity = dto.StockQuantity.Value;
                    }
                    if (dto.Status.HasValue)
                    {
                        product.Status = dto.Status.Value;
                    }
                    await unitOfWork.Repository<Product>().Update(product, id);
                    await unitOfWork.CommitAsync();
                    return "Update Successfull";
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                throw;
            }    
        }

        public async Task<GetAllProductResponseModel> GetProductDetailsResponseModel(int id)
        {
            try
            {
                var product = await unitOfWork.Repository<Product>().GetById(id);
                if (product != null)
                {
                    var user = await unitOfWork.Repository<User>().FindAsync(u => u.Id.Equals(product.UserId));
                    var category = await unitOfWork.Repository<Category>().FindAsync(c => c.Id.Equals(product.CategoryId));
                    var Subcategory = await unitOfWork.Repository<SubCategory>().FindAsync(c => c.Id.Equals(product.SubcategoryId));
                    GetAllProductResponseModel model = new GetAllProductResponseModel();
                    model = product.MapToGetAllProduct(_mapper);
                    model.UserName = user.UserName;
                    return model;
                }
                return null;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
