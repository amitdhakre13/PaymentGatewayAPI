using PaymentGatewayAPI.Dto;
using System.Threading.Tasks;

namespace PaymentGatewayAPI.Services
{
    public interface IPaymentService
    {
        Task<bool> ProcessPayment(CreditCardPaymentDto creditCardPaymentDto);
    }
}
