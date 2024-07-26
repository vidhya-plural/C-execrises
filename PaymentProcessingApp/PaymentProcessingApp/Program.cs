using System;

namespace PaymentProcessingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create instances of the PaymentMethods and PaymentProcessor classes
            PaymentMethods paymentMethods = new PaymentMethods();
            PaymentProcessor paymentProcessor = new PaymentProcessor();

            // Create delegate instances for each payment method
            PaymentHandler mastercardHandler = new PaymentHandler(paymentMethods.ProcessMastercardPayment);
            PaymentHandler visaHandler = new PaymentHandler(paymentMethods.ProcessVisaPayment);
            PaymentHandler discoverHandler = new PaymentHandler(paymentMethods.ProcessDiscoverPayment);

            // Test Mastercard payment
            bool isMastercardOk = paymentProcessor.ProcessPayment(mastercardHandler, "1234-1111-2222-1234", 100.00);
            Console.WriteLine(isMastercardOk ? "Mastercard payment processed successfully." : "[ALERT] Mastercard payment processing failed.");

            // Test Visa payment
            bool isVisaOk = paymentProcessor.ProcessPayment(visaHandler, "5678-1111-2222-5678", 200.00);
            Console.WriteLine(isVisaOk ? "Visa payment processed successfully." : "[ALERT] Visa payment processing failed.");

            // Test Discover payment
            bool isDiscoverOk = paymentProcessor.ProcessPayment(discoverHandler, "9876-1111-2222-9876", 300.00);
            Console.WriteLine(isDiscoverOk ? "Discover payment processed successfully." : "[ALERT] Discover payment processing failed.");

            Console.ReadLine(); // Keep the console open
        }
    }
}

