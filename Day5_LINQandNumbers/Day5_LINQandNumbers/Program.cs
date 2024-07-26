using System;
using System.Linq;

namespace LINQandNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            // Generate an array of 50 random numbers between 1 and 10,000
            int[] numbers = GenerateRandomNumbers(50, 1, 10000);

            // Perform LINQ queries and display results
            ListNumbersAscending(numbers);
            ListNumbersUnder100Descending(numbers);
            ListEvenNumbers(numbers);
            FindMinMaxSum(numbers);

            Console.ReadLine(); // Keep the console window open
        }

        // Generate an array of random numbers
        static int[] GenerateRandomNumbers(int count, int minValue, int maxValue)
        {
            Random random = new Random();
            return Enumerable.Range(1, count).Select(_ => random.Next(minValue, maxValue + 1)).ToArray();
        }

        // List the numbers in ascending order
        static void ListNumbersAscending(int[] numbers)
        {
            var ascendingNumbers = numbers.OrderBy(n => n);
            Console.WriteLine("Numbers in ascending order:");
            foreach (var num in ascendingNumbers)
            {
                Console.WriteLine(num);
            }
            Console.WriteLine(); // Empty line for readability
        }

        // List the numbers under 100 in descending order
        static void ListNumbersUnder100Descending(int[] numbers)
        {
            var under100Descending = numbers.Where(n => n < 100).OrderByDescending(n => n);
            Console.WriteLine("Numbers under 100 in descending order:");
            foreach (var num in under100Descending)
            {
                Console.WriteLine(num);
            }
            Console.WriteLine(); // Empty line for readability
        }

        // List the even numbers in original order
        static void ListEvenNumbers(int[] numbers)
        {
            var evenNumbers = numbers.Where(n => n % 2 == 0);
            Console.WriteLine("Even numbers in original order:");
            foreach (var num in evenNumbers)
            {
                Console.WriteLine(num);
            }
            Console.WriteLine(); // Empty line for readability
        }

        // Find the minimum number, maximum number, and the sum of all the numbers
        static void FindMinMaxSum(int[] numbers)
        {
            var minNumber = numbers.Min();
            var maxNumber = numbers.Max();
            var sum = numbers.Sum();

            Console.WriteLine($"Minimum number: {minNumber}");
            Console.WriteLine($"Maximum number: {maxNumber}");
            Console.WriteLine($"Sum of all numbers: {sum}");
            Console.WriteLine(); // Empty line for readability
        }
    }
}
