using AutoMapper;
using BusinessLayer.RequestModels.Report;
using BusinessLayer.ResponseModels.Product;
using BusinessLayer.ResponseModels.Report;
using DataLayer.Model;
using DataLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Implements
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReportService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public Task<string> AddReport(ReportRequestModel dto)
        {
            throw new NotImplementedException();
        }

        public Task<string> DeleteReport(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ReportResponseModel>> GetAll()
        {
            try
            {
                var Report = _unitOfWork.Repository<Report>().GetAll().ToList();
                List<ReportResponseModel> Final = new List<ReportResponseModel>();
                foreach (var report in Report)
                {
                    var user = await _unitOfWork.Repository<User>().FindAsync(u => u.Id.Equals(report.UserId));
                    var post = await _unitOfWork.Repository<Post>().FindAsync(c => c.Id.Equals(report.PostId));
                    ReportResponseModel result = new ReportResponseModel();
                    result = _mapper.Map<ReportResponseModel>(report);
                    result.UserName = user.UserName;
                    result.title = post.Title;
                    Final.Add(result);
                }
                return Final;

            }
            catch (Exception ex)
            {
                {
                    throw new Exception("Error DB!");
                }
            }
        }

        public Task<List<ReportResponseModel>> GetAllValidReport()
        {
            throw new NotImplementedException();
        }

        public Task<List<ReportResponseModel>> GetReportByPostId()
        {
            throw new NotImplementedException();
        }

        public Task<List<ReportResponseModel>> GetReportByUserId()
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateReport(int id, ReportRequestModel dto)
        {
            throw new NotImplementedException();
        }
    }
}
