using DataLayer.Model;
using DataLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Implements
{
    public class OrderService : IOrderService
    {
        private IUnitOfWork unitOfWork;
        public OrderService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public List<Order> GetAllOrder()
        {
            return unitOfWork.Repository<Order>().FindAll(o => o.Status == true).ToList();
        }
    }
}
