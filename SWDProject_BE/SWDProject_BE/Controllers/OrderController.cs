using BusinessLayer.Services;
using DataLayer.Model;
using Microsoft.AspNetCore.Mvc;

namespace SWDProject_BE.Controllers
{
    [Route("api/Order/")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderService OrderService { get; set; }


        public OrderController(IOrderService orderService)
        {
            OrderService = orderService;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAllProduct()
        {
            try
            {
                List<Order> list = OrderService.GetAllOrder().ToList();
                return Ok(list);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
