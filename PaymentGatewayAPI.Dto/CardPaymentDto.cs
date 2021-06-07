namespace PaymentGatewayAPI.Dto
{
    /// <summary>
    /// Abstract card payment dto
    /// </summary>
    public abstract class CardPaymentDto : PaymentDto
    {
        /// <summary>
        /// card number
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// card holder name
        /// </summary>
        public string CardHolderName { get; set; }

        /// <summary>
        /// card expiry year
        /// </summary>
        public int CardExpiryYear { get; set; }

        /// <summary>
        /// card expiry month
        /// </summary>
        public int CardExpiryMonth { get; set; }

        /// <summary>
        /// card cvv
        /// </summary>
        public string CardCvv { get; set; }
    }
}
