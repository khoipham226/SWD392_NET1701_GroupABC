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

        public async Task<String> addProduct(AddProductDto dto, int userId)
        {
            try
            {
                Product product = new Product();
                product.UserId = userId;
                product.SubCategoryId = dto.SubcategoryId;
                product.CategoryId = dto.CategoryId;
                product.Name = dto.Name;
                product.Price = dto.Price;
                product.Description = dto.Description;
                product.Condition = dto.Condition;
                product.Location = dto.Location;
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

        public async Task<List<GetAllProductResponseModel>> GetAllProductsValid()
        {
            try
            {
                var Product = unitOfWork.Repository<Product>().FindAll(p => p.Status == true && p.Price > 0).ToList();
                List<GetAllProductResponseModel> Final = new List<GetAllProductResponseModel>();
                foreach (var product in Product)
                {
                    var user = await unitOfWork.Repository<User>().FindAsync(u => u.Id.Equals(product.UserId));
                    var category = await unitOfWork.Repository<Category>().FindAsync(c => c.Id.Equals(product.CategoryId));
                    var Subcategory = await unitOfWork.Repository<SubCategory>().FindAsync(c => c.Id.Equals(product.SubCategoryId));
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

        public async Task<List<GetAllProductResponseModel>> GetAllProducts()
        {
            try
            {
                var Product = unitOfWork.Repository<Product>().GetAll().ToList();
                List<GetAllProductResponseModel> Final = new List<GetAllProductResponseModel>();
                foreach (var product in Product)
                {
                    var user = await unitOfWork.Repository<User>().FindAsync(u => u.Id.Equals(product.UserId));
                    var category = await unitOfWork.Repository<Category>().FindAsync(c => c.Id.Equals(product.CategoryId));
                    var Subcategory = await unitOfWork.Repository<SubCategory>().FindAsync(c => c.Id.Equals(product.SubCategoryId));
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

        public async Task<string> updateProduct(int id,UpdateProductDto dto, int userId)
        {
            try
            {
                var product = await unitOfWork.Repository<Product>().GetById(id);
                if(product != null) 
                {
                    if (userId != 0)
                    {
                        product.UserId = userId;
                    }
                    if (dto.CategoryId != 0)
                    {
                        product.CategoryId = dto.CategoryId.Value;
                    }
                    if (dto.SubcategoryId != 0)
                    {
                        product.SubCategoryId = dto.SubcategoryId.Value;
                    }
                    if (dto.Name != null)
                    {
                        product.Name = dto.Name;
                    }
                    if (dto.Price != 0)
                    {
                        product.Price = (double)dto.Price;
                    }
                    if (dto.Description != null)
                    {
                        product.Description = dto.Description;
                    }
                    if (dto.Condition != null)
                    {
                        product.Condition = dto.Condition;
                    }
                    if (dto.UrlImg != null)
                    {
                        product.UrlImg = dto.UrlImg;
                    }
                    if (dto.StockQuantity != 0)
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
                    var Subcategory = await unitOfWork.Repository<SubCategory>().FindAsync(c => c.Id.Equals(product.SubCategoryId));
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

        public async Task<List<GetAllProductResponseModel>> GetProductByUserId(int userId)
        {
            try
            {
                var listProduct = unitOfWork.Repository<Product>().FindAll(p => p.UserId == userId && p.Price>0).ToList();
                if(listProduct != null)
                {
                    List<GetAllProductResponseModel> final = new List<GetAllProductResponseModel>();
                    foreach (var product in listProduct)
                    {
                        var user = await unitOfWork.Repository<User>().FindAsync(u => u.Id.Equals(product.UserId));
                        var category = await unitOfWork.Repository<Category>().FindAsync(c => c.Id.Equals(product.CategoryId));
                        var Subcategory = await unitOfWork.Repository<SubCategory>().FindAsync(c => c.Id.Equals(product.SubCategoryId));
                        GetAllProductResponseModel result = new GetAllProductResponseModel();
                        result = product.MapToGetAllProduct(_mapper);
                        result.UserName = user.UserName;
                        result.CategoryName = category.Name;
                        result.SubcategoryName = Subcategory.Name;
                        final.Add(result);
                    }
                    return final;
                }
                else
                {
                    return null;
                }

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<GetAllProductResponseModel>> GetAllProductsForExchange(int userId)
        {
            try
            {
                var Product = unitOfWork.Repository<Product>().FindAll(p => p.Status == true && p.Price == 0 && p.UserId == userId).ToList();
                List<GetAllProductResponseModel> Final = new List<GetAllProductResponseModel>();
                foreach (var product in Product)
                {
                    var user = await unitOfWork.Repository<User>().FindAsync(u => u.Id.Equals(product.UserId));
                    var category = await unitOfWork.Repository<Category>().FindAsync(c => c.Id.Equals(product.CategoryId));
                    var Subcategory = await unitOfWork.Repository<SubCategory>().FindAsync(c => c.Id.Equals(product.SubCategoryId));
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
    }
}
