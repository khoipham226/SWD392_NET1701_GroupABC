using AutoMapper;
using BusinessLayer.RequestModels.Report;
using BusinessLayer.ResponseModels.Product;
using BusinessLayer.ResponseModels.Report;
using DataLayer.Model;
using DataLayer.UnitOfWork;
using Microsoft.IdentityModel.Tokens;
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
        public async Task<string> AddReportByUser(ReportRequestaUser dto, int userId)
        {
            try
            {
                var result = _mapper.Map<Report>(dto);
                result.Status = true;
                result.UserId = userId;
                result.Date = DateTime.Now;
                await _unitOfWork.Repository<Report>().InsertAsync(result);
                await _unitOfWork.CommitAsync();
                return "Add successful!";


            }catch (Exception ex)
            {
                throw new Exception("Error DB!");
            }
        }

        public async Task<string> DeleteReport(int id)
        {
            var report = await _unitOfWork.Repository<Report>().GetById(id);
            if(report != null)
            {
                report.Status = false;
                await _unitOfWork.Repository<Report>().Update(report,id);
                await _unitOfWork.CommitAsync();
                return "Delete successfull!";
            }
            else
            {
                return null;
            }
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

        public async Task<string> UpdateReportByUser(int id, ReportRequestaUser dto)
        {
            var report = await _unitOfWork.Repository<Report>().GetById(id);
            if(report != null)
            {
                if(dto.PostId != 0)
                {
                    report.PostId = dto.PostId;
                }
                if (!dto.Description.IsNullOrEmpty())
                {
                    report.Description = dto.Description;
                }
                await _unitOfWork.Repository<Report>().Update(report,id);
                await _unitOfWork.CommitAsync();
                return "Update Successful!";
            }
            return null;
        }
    }
}
