using System;

namespace NameAndAge
{
    class Program
    {
        static void Main(string[] args)
        {
            string answer;

            do
            {
                // Prompt user for name
                Console.Write("Enter your name: ");
                string name = Console.ReadLine();

                // Prompt user for age they will be on December 31st
                Console.Write("Enter your age on December 31st: ");
                int ageOnDec31 = Convert.ToInt32(Console.ReadLine());

                // Calculate birth year
                int birthYear = GetBirthYear(ageOnDec31);

                // Display the result
                Console.WriteLine($"{name} was born in {birthYear}");

                // Ask if user wants to enter another
                Console.Write("Do you want to enter another (yes/no)? ");
                answer = Console.ReadLine().Trim().ToLower();

                Console.WriteLine(); // Blank line for separation in output

            } while (answer == "yes");
        }

        // Method to calculate birth year based on age on December 31st
        static int GetBirthYear(int ageOnDec31)
        {
            // Calculate current year
            int currentYear = DateTime.Today.Year;

            // Calculate birth year
            int birthYear = currentYear - ageOnDec31;

            return birthYear;
        }
    }
}
