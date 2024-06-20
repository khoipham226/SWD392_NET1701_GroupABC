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

        public async Task<List<ReportResponseModel>> GetAllValidReport()
        {
            try
            {
                var Report = _unitOfWork.Repository<Report>().FindAll(r => r.Status == true).ToList();
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

        public async Task<List<ReportResponseModel>> GetReportByPostId(int postId)
        {
            try
            {
                var findPost = await _unitOfWork.Repository<Post>().GetById(postId);
                if (findPost != null)
                {
                    var Report = _unitOfWork.Repository<Report>().FindAll(r => r.PostId == postId).ToList();
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
                else
                {
                    return null;
                }


            }
            catch (Exception ex)
            {
                {
                    throw new Exception("Error DB!");
                }
            }
        }

        public async Task<List<ReportResponseModel>> GetReportByUserId(int userId)
        {
            try
            {
                var findUser = await _unitOfWork.Repository<User>().GetById(userId);
                if(findUser != null)
                {
                    var Report = _unitOfWork.Repository<Report>().FindAll(r => r.UserId == userId).ToList();
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
                else
                {
                    return null;
                }
                

            }
            catch (Exception ex)
            {
                {
                    throw new Exception("Error DB!");
                }
            }
        }

        public Task<string> UpdateReport(int id, ReportRequestModel dto)
        {
            throw new NotImplementedException();
        }
    }
}
