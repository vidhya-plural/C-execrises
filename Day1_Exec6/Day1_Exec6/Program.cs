using System;

namespace AddressDecypher
{
    class Program
    {
        static void Main(string[] args)
        {
            // Define the encoded address string
            string encodedAddress = "Betty Smallwood|3329 Duchess|Erath, Texas";

            // Split the encoded address using the pipe character '|'
            string[] fields = encodedAddress.Split('|');

            // Ensure we have exactly 3 fields (name, address, city state)
            if (fields.Length != 3)
            {
                Console.WriteLine("Invalid encoded address format.");
                return;
            }

            // Extract individual fields
            string name = fields[0];
            string address = fields[1];
            string cityState = fields[2];

            // Split cityState into city and state
            string[] cityStateParts = cityState.Split(',');

            if (cityStateParts.Length != 2)
            {
                Console.WriteLine("Invalid city, state format in the encoded address.");
                return;
            }

            string city = cityStateParts[0].Trim();
            string state = cityStateParts[1].Trim();

            // Display the extracted fields
            Console.WriteLine($"Name: {name}");
            Console.WriteLine($"Address: {address}");
            Console.WriteLine($"City: {city}");
            Console.WriteLine($"State: {state}");
        }
    }
}
