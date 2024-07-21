using System;

namespace LoanPaymentCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            // Prompt user for input
            Console.WriteLine("How much are you borrowing?");
            decimal amountBorrowed = Convert.ToDecimal(Console.ReadLine());

            Console.WriteLine("What is your interest rate? (Annual percentage)");
            double annualInterestRate = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("How long is your loan (in years)?");
            int loanYears = Convert.ToInt32(Console.ReadLine());

            // Calculate monthly interest rate
            double monthlyInterestRate = annualInterestRate / 1200;

            // Calculate number of monthly payments
            int numberOfPayments = loanYears * 12;

            // Calculate estimated monthly payment
            decimal estimatedPayment = CalculateMonthlyPayment(amountBorrowed, monthlyInterestRate, numberOfPayments);

            // Display the estimated payment formatted as money
            Console.WriteLine($"Your estimated payment is {estimatedPayment:C}");

            Console.ReadKey(); // To keep console window open until a key press
        }

        // Function to calculate monthly payment
        static decimal CalculateMonthlyPayment(decimal amountBorrowed, double monthlyInterestRate, int numberOfPayments)
        {
            // Formula: M = P * (r * (1 + r)^n) / ((1 + r)^n - 1)
            double r = monthlyInterestRate;
            double n = numberOfPayments;
            decimal P = amountBorrowed;

            decimal monthlyPayment = P * (decimal)(r * Math.Pow(1 + r, n)) / (decimal)(Math.Pow(1 + r, n) - 1);
            return monthlyPayment;
        }
    }
}
