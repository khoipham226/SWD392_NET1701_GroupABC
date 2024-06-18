using AutoMapper;
using BusinessLayer.ResponseModels.Product;
using DataLayer.Model;

namespace SWDProject_BE.AppStarts
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, GetAllProductResponseModel>();
        
        }
    }
}
