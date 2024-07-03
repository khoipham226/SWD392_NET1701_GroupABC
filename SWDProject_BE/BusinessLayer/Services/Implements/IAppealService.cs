using BusinessLayer.ResponseModels.Appeal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Implements
{
    public interface IAppealService
    {
        Task<List<AppealResponseModel>> GetAll();
    }
}
