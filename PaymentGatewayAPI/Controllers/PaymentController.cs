using Microsoft.AspNetCore.Mvc;
using PaymentGatewayAPI.Dto;
using PaymentGatewayAPI.Services;
using System;
using System.Threading.Tasks;

namespace PaymentGatewayAPI.Controllers
{
    [ApiController]
    [Route("Payment")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService ?? throw new ArgumentNullException(nameof(paymentService));
        }

        /// <summary>
        /// Process payment
        /// </summary>
        /// <param name="creditCardPaymentDto">creditCardPaymentDto</param>
        /// <returns>Failure or success</returns>
        [HttpPost]
        [Consumes("application/payment.creditcard+json")]
        public async Task<bool> ProcessPayment(CreditCardPaymentDto creditCardPaymentDto)
        {
            bool processPayment = await _paymentService.ProcessPayment(creditCardPaymentDto).ConfigureAwait(false);
            return processPayment;
        }
    }
}
