using System;

namespace PaymentProcessingApp
{
    public class PaymentMethods
    {
        private Random _random = new Random();

        public bool ProcessMastercardPayment(string accountNumber, double amount)
        {
            // Simulate payment processing
            Console.WriteLine($"Processing Mastercard payment for account {accountNumber} with amount {amount:C}");
            return _random.NextDouble() >= 0.10; // 90% success rate
        }

        public bool ProcessVisaPayment(string accountNumber, double amount)
        {
            // Simulate payment processing
            Console.WriteLine($"Processing Visa payment for account {accountNumber} with amount {amount:C}");
            return _random.NextDouble() >= 0.10; // 90% success rate
        }

        public bool ProcessDiscoverPayment(string accountNumber, double amount)
        {
            // Simulate payment processing
            Console.WriteLine($"Processing Discover payment for account {accountNumber} with amount {amount:C}");
            return _random.NextDouble() >= 0.10; // 90% success rate
        }
    }
}


namespace PaymentProcessingApp
{
    // Define the delegate type
    public delegate bool PaymentHandler(string accountNumber, double amount);
}
