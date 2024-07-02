using BusinessLayer.RequestModels;
using DataLayer.Model;
using DataLayer.Repository;
using DataLayer.UnitOfWork;

public class PaymentService : IPaymentService
{
    private readonly PayPalService _payPalService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<Payment> _paymentRepository;
    private readonly IGenericRepository<DataLayer.Model.Order> _orderRepository;
    private readonly IGenericRepository<OrderDetail> _orderDetailRepository;

    public PaymentService(IUnitOfWork unitOfWork, PayPalService payPalService)
    {
        _payPalService = payPalService;
        _unitOfWork = unitOfWork;
        _paymentRepository = _unitOfWork.Repository<Payment>();
        _orderRepository = _unitOfWork.Repository<DataLayer.Model.Order>();
        _orderDetailRepository = _unitOfWork.Repository<OrderDetail>();
    }

    public async Task<PayPalCheckoutSdk.Orders.Order> CreatePaymentAsync(decimal amount, string returnUrl, string cancelUrl)
    {
        var payment = await _payPalService.CreateOrder(amount, returnUrl, cancelUrl);
        return payment;
    }

    public async Task<PayPalCheckoutSdk.Orders.Order> ExecutePaymentAsync(string paymentId, string payerId, OrderRequestModel orderRequest)
    {
        var executedPayment = await _payPalService.CaptureOrder(paymentId);

        var order = new DataLayer.Model.Order
        {
            UserId = orderRequest.UserId,
            PaymentId = null, // add later
            TotalPrice = orderRequest.TotalPrice,
            Date = DateTime.Now,
            Status = true
        };
        await _orderRepository.InsertAsync(order);
        await _unitOfWork.CommitAsync();

        var payment = new Payment
        {
            Date = DateTime.Now.ToString("yyyy-MM-dd"),
            Amount = orderRequest.TotalPrice,
            Method = "PayPal",
            Status = true,
            Description = "PayerId: " + payerId + " - PaymentId: " + paymentId,
        };
        await _paymentRepository.InsertAsync(payment);
        await _unitOfWork.CommitAsync();

        order.PaymentId = payment.Id;
        _ = _orderRepository.Update(order, order.Id);
        await _unitOfWork.CommitAsync();

        foreach (var detail in orderRequest.OrderDetails)
        {
            var orderDetail = new OrderDetail
            {
                OrderId = order.Id,
                ProductId = detail.ProductId,
                Status = true
            };

            await _orderDetailRepository.InsertAsync(orderDetail);
        }
        await _unitOfWork.CommitAsync();

        return executedPayment;
    }

    public async Task<PayPalCheckoutSdk.Payments.Refund> RefundPaymentAsync(string captureId, decimal amount)
    {
        var refund = await _payPalService.RefundPayment(captureId, amount);

        var payment = _paymentRepository.GetAll().FirstOrDefault(p => p.Description == captureId);
        if (payment == null) throw new Exception("Payment not found");

        payment.Status = false;
        _ = _paymentRepository.Update(payment, payment.Id);
        await _unitOfWork.CommitAsync();

        return refund;
    }
}
