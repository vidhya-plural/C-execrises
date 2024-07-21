using System;
using System.Collections.Generic;

namespace ManagingFamily
{
    class Person
    {
        // Properties
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }

        // Constructor
        public Person(string name, int age, string gender)
        {
            Name = name;
            Age = age;
            Gender = gender;
        }

        // Method to display the person's information
        public void Display()
        {
            Console.WriteLine($"Name: {Name}, Age: {Age}, Gender: {Gender}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Person> familyMembers = new List<Person>();

            bool continueAdding = true;
            do
            {
                // Prompt user to enter person's details
                Console.Write("Enter name: ");
                string name = Console.ReadLine();

                Console.Write("Enter age: ");
                int age = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter gender: ");
                string gender = Console.ReadLine();

                // Create Person object and add to list
                Person newPerson = new Person(name, age, gender);
                familyMembers.Add(newPerson);

                // Ask if user wants to add another person
                Console.Write("Do you want to add another person? (yes/no): ");
                string answer = Console.ReadLine().ToLower();

                if (answer != "yes")
                {
                    continueAdding = false;
                }

            } while (continueAdding);

            // Display menu for operations
            bool continueManaging = true;
            do
            {
                Console.WriteLine("\nChoose an option:");
                Console.WriteLine("1. Display all people");
                Console.WriteLine("2. Display people of a selected gender");
                Console.WriteLine("3. Display people between a specified age range");
                Console.WriteLine("4. Exit");

                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine().Trim();

                switch (choice)
                {
                    case "1":
                        DisplayAllPeople(familyMembers);
                        break;
                    case "2":
                        DisplayPeopleByGender(familyMembers);
                        break;
                    case "3":
                        DisplayPeopleByAgeRange(familyMembers);
                        break;
                    case "4":
                        continueManaging = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a number from 1 to 4.");
                        break;
                }

            } while (continueManaging);
        }

        // Method to display all people in the list
        static void DisplayAllPeople(List<Person> people)
        {
            Console.WriteLine("\nAll people:");
            foreach (Person person in people)
            {
                person.Display();
            }
        }

        // Method to display people of a selected gender
        static void DisplayPeopleByGender(List<Person> people)
        {
            Console.Write("\nEnter gender to filter (Male/Female): ");
            string gender = Console.ReadLine().Trim();

            Console.WriteLine($"\nPeople with gender '{gender}':");
            foreach (Person person in people)
            {
                if (person.Gender.Equals(gender, StringComparison.OrdinalIgnoreCase))
                {
                    person.Display();
                }
            }
        }

        // Method to display people between a specified age range
        static void DisplayPeopleByAgeRange(List<Person> people)
        {
            Console.Write("\nEnter minimum age: ");
            int minAge = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter maximum age: ");
            int maxAge = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"\nPeople between ages {minAge} and {maxAge}:");
            foreach (Person person in people)
            {
                if (person.Age >= minAge && person.Age <= maxAge)
                {
                    person.Display();
                }
            }
        }
    }
}
