using BusinessLayer.ResponseModels.Appeal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public interface IAppealService
    {
        Task<List<AppealResponseModel>> GetAll();
        Task<List<AppealResponseModel>> GetAllByUserId(int userId);
        Task<string> AcceptAppeal(int AppealId);
    }
}
