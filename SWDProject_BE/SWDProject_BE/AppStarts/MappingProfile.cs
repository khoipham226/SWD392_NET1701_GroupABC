using AutoMapper;
using BusinessLayer.ResponseModels.Category;
using BusinessLayer.ResponseModels.Product;
using BusinessLayer.ResponseModels.Subcategory;
using DataLayer.Model;

namespace SWDProject_BE.AppStarts
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, GetAllProductResponseModel>().ReverseMap();
            CreateMap<SubcategoryResponseModel, SubCategory>().ReverseMap();
        
        }
    }
}
