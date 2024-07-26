using System;

namespace PaymentProcessingApp
{
    public class PaymentProcessor
    {
        public bool ProcessPayment(PaymentHandler paymentHandler, string accountNumber, double amount)
        {
            return paymentHandler(accountNumber, amount);
        }
    }
}
