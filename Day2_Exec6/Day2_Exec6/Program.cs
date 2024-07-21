using System;

namespace TimeMath
{
    class Program
    {
        static void Main(string[] args)
        {
            // Prompt user to enter a time
            Console.Write("Enter a time in hh:mm format: ");
            string inputTime = Console.ReadLine();

            // Parse the input time
            if (TryParseTime(inputTime, out int hours, out int minutes))
            {
                // Prompt user to enter minutes to add
                Console.Write("Enter minutes to add: ");
                int minutesToAdd = Convert.ToInt32(Console.ReadLine());

                // Perform time calculation
                AddMinutesToTime(ref hours, ref minutes, minutesToAdd);

                // Display the new time
                Console.WriteLine($"New time: {hours:D2}:{minutes:D2}");
            }
            else
            {
                Console.WriteLine("Invalid time format. Please enter time in hh:mm format.");
            }
        }

        // Method to parse the input time string into hours and minutes
        static bool TryParseTime(string inputTime, out int hours, out int minutes)
        {
            hours = 0;
            minutes = 0;

            string[] timeParts = inputTime.Split(':');
            if (timeParts.Length == 2 &&
                int.TryParse(timeParts[0], out hours) &&
                int.TryParse(timeParts[1], out minutes) &&
                hours >= 0 && hours <= 23 &&
                minutes >= 0 && minutes <= 59)
            {
                return true;
            }

            return false;
        }

        // Method to add minutes to the given time
        static void AddMinutesToTime(ref int hours, ref int minutes, int minutesToAdd)
        {
            minutes += minutesToAdd;
            hours += minutes / 60;
            minutes = minutes % 60;
            hours = hours % 24;
        }
    }
}
