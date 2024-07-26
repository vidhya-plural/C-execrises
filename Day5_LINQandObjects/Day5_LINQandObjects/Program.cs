using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQandObjects
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a list of Person objects
            List<Person> people = new List<Person>
            {
                new Person { Name = "Alice", Gender = "Female", Age = 22, Hometown = "New York" },
                new Person { Name = "Bob", Gender = "Male", Age = 30, Hometown = "Los Angeles" },
                new Person { Name = "Charlie", Gender = "Male", Age = 18, Hometown = "Chicago" },
                new Person { Name = "Diana", Gender = "Female", Age = 27, Hometown = "New York" },
                new Person { Name = "Eve", Gender = "Female", Age = 22, Hometown = "San Francisco" },
                new Person { Name = "Frank", Gender = "Male", Age = 20, Hometown = "San Francisco" },
                new Person { Name = "Grace", Gender = "Female", Age = 23, Hometown = "Chicago" },
                new Person { Name = "Hank", Gender = "Male", Age = 28, Hometown = "New York" },
                new Person { Name = "Ivy", Gender = "Female", Age = 24, Hometown = "Los Angeles" },
                new Person { Name = "Jack", Gender = "Male", Age = 35, Hometown = "San Francisco" },
                new Person { Name = "Karen", Gender = "Female", Age = 21, Hometown = "Los Angeles" },
                new Person { Name = "Leo", Gender = "Male", Age = 26, Hometown = "Chicago" },
                new Person { Name = "Mona", Gender = "Female", Age = 19, Hometown = "New York" },
                new Person { Name = "Nina", Gender = "Female", Age = 29, Hometown = "San Francisco" },
                new Person { Name = "Oscar", Gender = "Male", Age = 32, Hometown = "Los Angeles" },
                new Person { Name = "Paul", Gender = "Male", Age = 25, Hometown = "Chicago" },
                new Person { Name = "Quinn", Gender = "Female", Age = 31, Hometown = "New York" },
                new Person { Name = "Rita", Gender = "Female", Age = 26, Hometown = "San Francisco" },
                new Person { Name = "Sam", Gender = "Male", Age = 27, Hometown = "Chicago" },
                new Person { Name = "Tina", Gender = "Female", Age = 20, Hometown = "Los Angeles" }
            };

            // Perform LINQ queries and display results
            ListMalesUnder25(people);
            ListDistinctHometowns(people);
            ListPeopleSortedByHometown(people);
            CalculateAverageAgeByGender(people);
            ListHometownsAndCounts(people);

            Console.ReadLine(); // Keep the console window open
        }

        // List the males under 25
        static void ListMalesUnder25(List<Person> people)
        {
            var malesUnder25 = people.Where(p => p.Gender == "Male" && p.Age < 25);
            Console.WriteLine("Males under 25:");
            foreach (var person in malesUnder25)
            {
                Console.WriteLine(person);
            }
            Console.WriteLine(); // Empty line for readability
        }

        // Provide a list of all the distinct hometowns in ascending order
        static void ListDistinctHometowns(List<Person> people)
        {
            var distinctHometowns = people.Select(p => p.Hometown).Distinct().OrderBy(h => h);
            Console.WriteLine("Distinct hometowns in ascending order:");
            foreach (var hometown in distinctHometowns)
            {
                Console.WriteLine(hometown);
            }
            Console.WriteLine(); // Empty line for readability
        }

        // Provide a list of people sorted by hometown, and within hometown by name
        static void ListPeopleSortedByHometown(List<Person> people)
        {
            var sortedPeople = people.OrderBy(p => p.Hometown).ThenBy(p => p.Name);
            Console.WriteLine("People sorted by hometown and name:");
            foreach (var person in sortedPeople)
            {
                Console.WriteLine(person);
            }
            Console.WriteLine(); // Empty line for readability
        }

        // What is the average age of all women and the average age of all men
        static void CalculateAverageAgeByGender(List<Person> people)
        {
            var averageAgeByGender = people.GroupBy(p => p.Gender)
                                           .Select(g => new
                                           {
                                               Gender = g.Key,
                                               AverageAge = g.Average(p => p.Age)
                                           });

            Console.WriteLine("Average age by gender:");
            foreach (var group in averageAgeByGender)
            {
                Console.WriteLine($"{group.Gender}: {group.AverageAge:F2}");
            }
            Console.WriteLine(); // Empty line for readability
        }

        // Provide a list of hometowns and how many people are from that hometown
        static void ListHometownsAndCounts(List<Person> people)
        {
            var hometownCounts = people.GroupBy(p => p.Hometown)
                                       .Select(g => new
                                       {
                                           Hometown = g.Key,
                                           Count = g.Count()
                                       })
                                       .OrderBy(h => h.Hometown);

            Console.WriteLine("Hometowns and number of people from each:");
            foreach (var hometown in hometownCounts)
            {
                Console.WriteLine($"{hometown.Hometown}: {hometown.Count}");
            }
            Console.WriteLine(); // Empty line for readability
        }
    }
}
