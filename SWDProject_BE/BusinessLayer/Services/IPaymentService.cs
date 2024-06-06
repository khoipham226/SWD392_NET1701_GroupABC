using BusinessLayer.RequestModels;
using DataLayer.Model;
using PayPalCheckoutSdk.Orders;
using PayPalCheckoutSdk.Payments;
using System.Threading.Tasks;

public interface IPaymentService
{
    Task<DataLayer.Model.Order> CreatePaymentAsync(decimal amount, string returnUrl, string cancelUrl);
    Task<DataLayer.Model.Order> ExecutePaymentAsync(string paymentId, string payerId);
    Task<PayPalCheckoutSdk.Payments.Refund> RefundPaymentAsync(string captureId, decimal amount);
}
