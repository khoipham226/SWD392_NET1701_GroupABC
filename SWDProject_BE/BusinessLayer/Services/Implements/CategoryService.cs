using AutoMapper;
using BusinessLayer.RequestModels.Category;
using BusinessLayer.ResponseModels.Category;
using BusinessLayer.ResponseModels.Subcategory;
using DataLayer.Model;
using DataLayer.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Implements
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            _unitOfWork = unitOfWork; 
            _mapper = mapper;
        }

        public async Task<string> AddCategory(CategoryRequestModel dto)
        {
            try
            {
                Category category = new Category()
                {
                    Description = dto.Description,
                    Name = dto.Name,
                    Status = true
                };
                await _unitOfWork.Repository<Category>().InsertAsync(category);
                await _unitOfWork.CommitAsync();
                return "Add Category Sucessfull";

            } catch (Exception ex) 
            {
                throw new Exception("Error DB!");
            }
        }

        public async Task<string> DeleteCategory(int id)
        {
            try
            {
                var category = await _unitOfWork.Repository<Category>().GetById(id);
                if (category != null)
                {
                    category.Status = false;
                    await _unitOfWork.Repository<Category>().Update(category,id);
                    await _unitOfWork.CommitAsync();
                    return "Delete Successful!";
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error DB!");
            }

        }

        public async Task<List<CategoryResponse>> GetAll()
        {
            try
            {
                var category = _unitOfWork.Repository<Category>().GetAll();
                var result = _mapper.Map<List<CategoryResponse>>(category);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error DB!");
            }
        }

        public async Task<List<CategoryResponse>> GetAllValidCategory()
        {
            try
            {
                var category =  _unitOfWork.Repository<Category>().FindAll(c => c.Status == true).ToList();
                var result = _mapper.Map<List<CategoryResponse>>(category);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error DB!");
            }
        }

        public async Task<List<CategoryResponseModel>> GetAllWithSubcategory()
        {
            try
            {
                List<CategoryResponseModel> result = new List<CategoryResponseModel>();
                var listCategory = _unitOfWork.Repository<Category>().FindAll(c => c.Status == true).ToList();
                foreach (var category in listCategory)
                {
                    var listSubcategory =  _unitOfWork.Repository<SubCategory>().FindAll(s => s.CategoryId == category.Id).ToList();
                    var listSubcategoryResponse = _mapper.Map<List<SubcategoryResponseModel>>(listSubcategory);


                    CategoryResponseModel categoryResponseModel = new CategoryResponseModel();
                    categoryResponseModel.Id = category.Id;
                    categoryResponseModel.Name = category.Name;
                    categoryResponseModel.SubCategories = listSubcategoryResponse;
                    result.Add(categoryResponseModel);

                }
                return result;
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }         
        }

        public async Task<CategoryResponse> GetById(int id)
        {
            try
            {
                var category = await _unitOfWork.Repository<Category>().GetById(id);
                if(category != null)
                {
                    var result = _mapper.Map<CategoryResponse>(category);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> UpdateCategory(int id, CategoryRequestModel dto)
        {
            try
            {
                var category = await _unitOfWork.Repository<Category>().GetById(id);
                if (category != null)
                {
                    if(dto.Name != null)
                    {
                        category.Name = dto.Name;
                    }
                    if (dto.Description != null)
                    {
                        category.Description = dto.Description;
                    }
                    await _unitOfWork.Repository<Category>().Update(category, id);
                    await _unitOfWork.CommitAsync();
                    return "Update Successful!";
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error DB!");
            }

        }
    }
}
