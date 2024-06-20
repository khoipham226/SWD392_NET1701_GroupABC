using BusinessLayer.RequestModels.Report;
using BusinessLayer.ResponseModels.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public interface IReportService
    {
        Task<List<ReportResponseModel>> GetAll();
        Task<List<ReportResponseModel>> GetAllValidReport();
        Task<List<ReportResponseModel>> GetReportByUserId();
        Task<List<ReportResponseModel>> GetReportByPostId();
        Task<string> AddReport(ReportRequestModel dto);
        Task<string> UpdateReport(int id,ReportRequestModel dto);
        Task<string> DeleteReport(int id);
    }
}
