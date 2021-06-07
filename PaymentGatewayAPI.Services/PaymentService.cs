using AutoMapper;
using Microsoft.Extensions.Options;
using PaymentGatewayAPI.Configuration;
using PaymentGatewayAPI.Dto;
using PaymentGatewayAPI.PaymentStrategy;
using System;
using System.Threading.Tasks;

namespace PaymentGatewayAPI.Services
{
    /// <summary>
    /// Represents helper function related to payment.
    /// </summary>
    public class PaymentService : IPaymentService
    {
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapper">mapper</param>
        /// <param name="options">options</param>
        public PaymentService(IMapper mapper, IOptions<AppSettings> options)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _appSettings = options == null ? throw new ArgumentNullException(nameof(options)) : options.Value;
        }

        /// <summary>
        /// Process payment
        /// </summary>
        /// <param name="creditCardPaymentDto">creditCardPaymentDto</param>
        /// <returns>True, if payment was successful</returns>
        public async Task<bool> ProcessPayment(CreditCardPaymentDto creditCardPaymentDto)
        {
            bool isPaymentSuccessful = false;

            IPaymentGatewayStrategy paymentGatewayStrategy = GetPaymentGatewayStrategy(_appSettings.PaymentGatewayStrategyValues, creditCardPaymentDto.AmountToPay);
            if (paymentGatewayStrategy != null)
            {
                isPaymentSuccessful = await paymentGatewayStrategy.ProcessPayment(creditCardPaymentDto.CardNumber, 
                    creditCardPaymentDto.CardHolderName, creditCardPaymentDto.CardExpiryMonth, creditCardPaymentDto.CardExpiryYear,
                    creditCardPaymentDto.CardCvv, creditCardPaymentDto.AmountToPay).ConfigureAwait(false);
            }

            return (isPaymentSuccessful);
        }

        private static IPaymentGatewayStrategy GetPaymentGatewayStrategy(dynamic paymentGatewayStrategyValues, decimal amount)
        {
            IPaymentGatewayStrategy paymentGatewayStrategy = null;
            if(amount <= paymentGatewayStrategyValues.CheapEndValue)
            {
                paymentGatewayStrategy = new CheapPaymentGatewayStrategy();
            }
            else if (amount >= paymentGatewayStrategyValues.ExpensiveStartValue && amount <= paymentGatewayStrategyValues.ExpensiveEndValue)
            {
                paymentGatewayStrategy = new ExpensivePaymentGatewayStrategy();
            }
            else if (amount >= paymentGatewayStrategyValues.PremiumStartValue)
            {
                paymentGatewayStrategy = new PremiumPaymentGatewayStrategy();
            }

            return paymentGatewayStrategy;
        }
    }
}
