using AutoMapper;
using BusinessLayer.RequestModels.Category;
using BusinessLayer.RequestModels.Report;
using BusinessLayer.RequestModels.Subcategory;
using BusinessLayer.ResponseModels.Category;
using BusinessLayer.ResponseModels.Product;
using BusinessLayer.ResponseModels.Report;
using BusinessLayer.ResponseModels.Subcategory;
using DataLayer.Model;

namespace SWDProject_BE.AppStarts
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, GetAllProductResponseModel>().ReverseMap();

            //Category
            CreateMap<SubcategoryResponseModel, SubCategory>().ReverseMap();
            CreateMap<CategoryRequestModel, Category>().ReverseMap();
            CreateMap<CategoryResponse, Category>().ReverseMap();


            //Subcategory
            CreateMap<SubCategoryRequestModel, SubCategory>().ReverseMap();
            CreateMap<SubcategoryResponseModel, SubCategory>().ReverseMap();


            //Report
            CreateMap<ReportRequestaUser, Report>().ReverseMap();
            CreateMap<ReportResponseModel, Report>().ReverseMap();

        }
    }
}
