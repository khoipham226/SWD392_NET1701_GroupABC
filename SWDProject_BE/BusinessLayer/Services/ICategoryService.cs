using BusinessLayer.ResponseModels.Category;
using BusinessLayer.Services.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryResponseModel>> GetAll();
    }
}
