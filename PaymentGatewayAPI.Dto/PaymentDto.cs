namespace PaymentGatewayAPI.Dto
{
    /// <summary>
    /// Abstract payment dto
    /// </summary>
    public abstract class PaymentDto
    {
        /// <summary>
        /// Amount to pay
        /// </summary>
        public decimal AmountToPay { get; set; }
    }
}
