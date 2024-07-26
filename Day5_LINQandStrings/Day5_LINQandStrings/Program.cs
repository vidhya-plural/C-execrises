using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQandStrings
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a list of fruits and vegetables
            List<string> items = new List<string>
            {
                "Apple", "Banana", "Strawberry", "Cherry", "Mango", "Blueberry", "Carrot",
                "Broccoli", "Cauliflower", "Potato", "Tomato", "Grapes", "Peach",
                "Plum", "Lettuce", "Spinach", "Cucumber", "Zucchini", "Pumpkin", "Radish", "Betty"
            };

            // Perform LINQ queries and display results
            ListStringsStartingWithB(items);
            ListStringsContainingBetty(items);
            ListStringsStartingAtoM(items);
            CountStringsStartingNtoZ(items);
            FindLongestString(items);

            Console.ReadLine(); // Keep the console window open
        }

        // List all strings that start with a B or b
        static void ListStringsStartingWithB(List<string> items)
        {
            var startWithB = items.Where(item => item.StartsWith('B') || item.StartsWith('b'));
            Console.WriteLine("Strings starting with B or b:");
            foreach (var item in startWithB)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine(); // Empty line for readability
        }

        // List all strings that contain the word "betty"
        static void ListStringsContainingBetty(List<string> items)
        {
            var containingBetty = items.Where(item => item.IndexOf("Betty", StringComparison.OrdinalIgnoreCase) >= 0);
            Console.WriteLine("Strings containing 'Betty':");
            foreach (var item in containingBetty)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine(); // Empty line for readability
        }

        // List strings that start with the letters A-M
        static void ListStringsStartingAtoM(List<string> items)
        {
            var startWithAtoM = items.Where(item => item.Length > 0 && item[0] >= 'A' && item[0] <= 'M');
            Console.WriteLine("Strings starting with letters A-M:");
            foreach (var item in startWithAtoM)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine(); // Empty line for readability
        }

        // How many strings start with the letters N-Z
        static void CountStringsStartingNtoZ(List<string> items)
        {
            var countNtoZ = items.Count(item => item.Length > 0 && item[0] >= 'N' && item[0] <= 'Z');
            Console.WriteLine($"Number of strings starting with letters N-Z: {countNtoZ}");
            Console.WriteLine(); // Empty line for readability
        }

        // What is the longest string in the list
        static void FindLongestString(List<string> items)
        {
            var longestString = items.OrderByDescending(item => item.Length).FirstOrDefault();
            Console.WriteLine($"Longest string: {longestString}");
            Console.WriteLine(); // Empty line for readability
        }
    }
}
