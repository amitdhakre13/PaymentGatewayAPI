namespace PaymentGatewayAPI.Configuration
{
    /// <summary>
    /// Represents appsettings.json
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Logging
        /// </summary>
        public Logging Logging { get; set; }

        /// <summary>
        /// Payment strategy values
        /// </summary>
        public PaymentGatewayStrategyValues PaymentGatewayStrategyValues { get; set; }

        /// <summary>
        /// Allowed hosts
        /// </summary>
        public string AllowedHosts { get; set; }
    }
    /// <summary>
    /// Represents logging
    /// </summary>
    public class Logging
    {
        /// <summary>
        /// Log level
        /// </summary>
        public LogLevel LogLevel { get; set; }
    }

    /// <summary>
    /// Represents log level
    /// </summary>
    public class LogLevel
    {
        /// <summary>
        /// Default
        /// </summary>
        public string Default { get; set; }

        /// <summary>
        /// Microsoft
        /// </summary>
        public string MMicrosoft { get; set; }

        /// <summary>
        /// Microsoft hosting lifetime
        /// </summary>
        public string MicrosoftHostingLifetime { get; set; }

    }

    /// <summary>
    /// Represents payment strategy values
    /// </summary>
    public class PaymentGatewayStrategyValues
    {
        /// <summary>
        /// Cheap end value
        /// </summary>
        public int CheapEndValue { get; set; }

        /// <summary>
        /// Expensive start value
        /// </summary>
        public int ExpensiveStartValue { get; set; }

        /// <summary>
        /// Expensive end value
        /// </summary>
        public int ExpensiveEndValue { get; set; }

        /// <summary>
        /// Premium start value
        /// </summary>
        public int PremiumStartValue { get; set; }
    }
}
