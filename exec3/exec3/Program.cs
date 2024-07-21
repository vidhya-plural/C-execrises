using System;

namespace ReadTheData
{
    class Program
    {
        static void Main(string[] args)
        {
            // Prompt user for two integers
            Console.WriteLine("Enter the first integer:");
            int num1 = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the second integer:");
            int num2 = int.Parse(Console.ReadLine());

            // Calculate and display results for integers
            Console.WriteLine($"Integer Results:");
            Console.WriteLine($"Sum: {num1} + {num2} = {num1 + num2}");
            Console.WriteLine($"Difference: {num1} - {num2} = {num1 - num2}");
            Console.WriteLine($"Product: {num1} * {num2} = {num1 * num2}");

            if (num2 != 0)
            {
                Console.WriteLine($"Quotient: {num1} / {num2} = {(double)num1 / num2}");
            }
            else
            {
                Console.WriteLine("Cannot divide by zero.");
            }

            // Prompt user for two doubles
            Console.WriteLine("\nEnter the first double:");
            double dbl1 = double.Parse(Console.ReadLine());

            Console.WriteLine("Enter the second double:");
            double dbl2 = double.Parse(Console.ReadLine());

            // Calculate and display results for doubles
            Console.WriteLine($"\nDouble Results:");
            Console.WriteLine($"Sum: {dbl1} + {dbl2} = {dbl1 + dbl2}");
            Console.WriteLine($"Difference: {dbl1} - {dbl2} = {dbl1 - dbl2}");
            Console.WriteLine($"Product: {dbl1} * {dbl2} = {dbl1 * dbl2}");

            if (dbl2 != 0)
            {
                Console.WriteLine($"Quotient: {dbl1} / {dbl2} = {dbl1 / dbl2}");
            }
            else
            {
                Console.WriteLine("Cannot divide by zero.");
            }

            Console.ReadKey(); // To keep console window open until a key press
        }
    }
}
