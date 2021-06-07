using System.Threading.Tasks;

namespace PaymentGatewayAPI.PaymentStrategy
{
    /// <summary>
    /// Represents contract for payment gateway.
    /// </summary>
    public interface IPaymentGatewayStrategy
    {
        /// <summary>
        /// Process payment
        /// </summary>
        /// <param name="creditCardNumber">Credit card number</param>
        /// <param name="cardHolderName">Credit card holder' name</param>
        /// <param name="expiryMonth">Expiry month</param>
        /// <param name="expiryYear">Expiry year</param>
        /// <param name="securityCode">Security code</param>
        /// <param name="amount">Amount</param>
        /// <returns>True, if payment was successful</returns>
        Task<bool> ProcessPayment(string creditCardNumber, string cardHolderName, int expiryMonth, int expiryYear, string securityCode, decimal amount);
    }
}
