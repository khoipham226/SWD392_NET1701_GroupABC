using AutoMapper;
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
        public async Task<List<CategoryResponseModel>> GetAll()
        {
            try
            {
                List<CategoryResponseModel> result = new List<CategoryResponseModel>();
                var listCategory = await _unitOfWork.Repository<Category>().GetAll().ToListAsync();
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
    }
}
