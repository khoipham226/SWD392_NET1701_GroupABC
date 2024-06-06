using BusinessLayer.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace SWDProject_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePayment(decimal amount, string returnUrl, string cancelUrl)
        {
            try
            {
                var payment = await _paymentService.CreatePaymentAsync(amount, returnUrl, cancelUrl);
                return Ok(payment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("execute")]
        public async Task<IActionResult> ExecutePayment(string paymentId, string payerId)
        {
            try
            {
                var payment = await _paymentService.ExecutePaymentAsync(paymentId, payerId);
                return Ok(payment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("refund")]
        public async Task<IActionResult> RefundPayment(string captureId, decimal amount)
        {
            try
            {
                var refund = await _paymentService.RefundPaymentAsync(captureId, amount);
                return Ok(refund);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
