using System;

namespace exec2
{
    class Program
    {
        static void Main(string[] args)
        {
            string name = "Zachary";
            int dayOfBirth = 20;
            int monthOfBirth = 5;
            int yearOfBirth = 1990;
            decimal salary = 69759.25m;

            // Version 1 formatting
            string formattedV1 = string.Format("V1 -{0} was born on {1:D2}-{2:D2}{3:D4} and earns ${4:N2}.",
                                               name, dayOfBirth, monthOfBirth, yearOfBirth, salary);

            // Version 2 formatting
            string formattedV2 = string.Format("V2 -{0} was born {1}-{2:D2}{3:D4} and earns ${4:N2}.",
                                               name, dayOfBirth, monthOfBirth, yearOfBirth, salary);

            Console.WriteLine(formattedV1);
            Console.WriteLine(formattedV2);

            Console.ReadKey(); // To keep console window open until a key press
        }
    }
}
