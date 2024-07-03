using AutoMapper;
using BusinessLayer.ResponseModels.Appeal;
using DataLayer.Model;
using DataLayer.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Implements
{
    public class AppealService : IAppealService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public AppealService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<AppealResponseModel>> GetAll()
        {
            try
            {
                var appealList = await _unitOfWork.Repository<Appeal>().GetAll().ToListAsync();
                List<AppealResponseModel> listAppealModel = new List<AppealResponseModel>();
                foreach (var appeal in appealList)
                {
                    AppealResponseModel appealModel = new AppealResponseModel();
                    appealModel = _mapper.Map<AppealResponseModel>(appeal);

                    var user = await _unitOfWork.Repository<User>().FindAsync(u => u.Id == appeal.UserId);
                    if (user == null)
                    {
                        appealModel.UserName = "not found";
                    }
                    else
                    {
                        appealModel.UserName = user.UserName;
                    }
                    var bannerUser = await _unitOfWork.Repository<BannedAccount>().FindAsync(b => b.Id == appeal.BannerAcountId);
                    if (bannerUser == null)
                    {
                        appealModel.BannerDescription = "not found";
                    }
                    else
                    {
                        appealModel.BannerDescription = bannerUser.Description;
                        appealModel.BannerDate = bannerUser.Date;
                    }
                    listAppealModel.Add(appealModel);
                }
                return listAppealModel;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<AppealResponseModel>> GetAllByUserId(int userId)
        {
            try
            {
                var checkUser = _unitOfWork.Repository<User>().Find(u => u.Id == userId).FirstOrDefault();
                if (checkUser == null) 
                {
                    return null;
                }
                var appealList =  _unitOfWork.Repository<Appeal>().FindAll(a => a.UserId == userId).ToList();
                List<AppealResponseModel> listAppealModel = new List<AppealResponseModel>();
                foreach (var appeal in appealList)
                {
                    AppealResponseModel appealModel = new AppealResponseModel();
                    appealModel = _mapper.Map<AppealResponseModel>(appeal);

                    var user = await _unitOfWork.Repository<User>().FindAsync(u => u.Id == appeal.UserId);
                    if (user == null)
                    {
                        appealModel.UserName = "not found";
                    }
                    else
                    {
                        appealModel.UserName = user.UserName;
                    }
                    var bannerUser = await _unitOfWork.Repository<BannedAccount>().FindAsync(b => b.Id == appeal.BannerAcountId);
                    if (bannerUser == null)
                    {
                        appealModel.BannerDescription = "not found";
                    }
                    else
                    {
                        appealModel.BannerDescription = bannerUser.Description;
                        appealModel.BannerDate = bannerUser.Date;
                    }
                    listAppealModel.Add(appealModel);
                }
                return listAppealModel;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
