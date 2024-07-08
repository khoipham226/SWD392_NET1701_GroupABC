using AutoMapper;
using BusinessLayer.RequestModels.Order;
using BusinessLayer.ResponseModels.Order;
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
    public class OrderService : IOrderService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }


        public async Task<List<OrderResponseModel>> GetAllOrder()
        {
            try
            {        
                var listOrder = await _unitOfWork.Repository<Order>().GetAll().ToListAsync();
                var listResult = _mapper.Map<List<OrderResponseModel>>(listOrder);
                foreach (var item in listResult)
                {
                    var user = await _unitOfWork.Repository<User>().GetById(item.UserId);
                    var payment = await _unitOfWork.Repository<Payment>().GetById((int)item.PaymentId);
                    if(user == null)
                    {
                        item.UserName = "not fount";
                    }
                    else
                    {
                        item.UserName = user.UserName;
                    }
                    if(payment == null)
                    {
                        item.Amount = 0;
                    }
                    else
                    {
                        item.Amount = payment.Amount;
                    }
                    var listOrderDetails = await _unitOfWork.Repository<OrderDetail>().GetAll().Where(od => od.OrderId == item.Id).ToListAsync();
                    item.OrderDetails = listOrderDetails;
                }
                return listResult;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
