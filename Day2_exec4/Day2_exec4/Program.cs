using System;
using System.Collections.Generic;
using System.Linq;

namespace FavoriteMovies
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> movies = new List<string>();

            // Prompt user to enter movies until they choose to stop
            bool continueAdding = true;
            do
            {
                Console.Write("Enter a movie (or 'stop' to finish): ");
                string movie = Console.ReadLine().Trim();

                if (movie.ToLower() == "stop")
                {
                    continueAdding = false;
                }
                else
                {
                    movies.Add(movie);
                }

            } while (continueAdding);

            // Sort the list of movies
            movies.Sort();

            // Display the sorted list of movies
            Console.WriteLine("\nSorted list of movies:");
            foreach (string movie in movies)
            {
                Console.WriteLine(movie);
            }

            // Allow user to search the list of movies
            bool continueSearching = true;
            do
            {
                Console.WriteLine("\nChoose an option:");
                Console.WriteLine("1. Partial Search");
                Console.WriteLine("2. Exact Search");
                Console.WriteLine("3. Exit");

                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine().Trim();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter a word or phrase to search for (partial search): ");
                        string partialSearch = Console.ReadLine().Trim().ToLower();
                        PerformPartialSearch(movies, partialSearch);
                        break;
                    case "2":
                        Console.Write("Enter the exact movie name to search for (exact search): ");
                        string exactSearch = Console.ReadLine().Trim().ToLower();
                        PerformExactSearch(movies, exactSearch);
                        break;
                    case "3":
                        continueSearching = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter 1, 2, or 3.");
                        break;
                }

            } while (continueSearching);
        }

        // Method to perform a partial search in the list of movies
        static void PerformPartialSearch(List<string> movies, string partialSearch)
        {
            List<string> results = movies
                .Where(movie => movie.ToLower().Contains(partialSearch))
                .ToList();

            Console.WriteLine("\nPartial search results:");
            if (results.Any())
            {
                foreach (string result in results)
                {
                    Console.WriteLine(result);
                }
            }
            else
            {
                Console.WriteLine("No movies found matching the partial search.");
            }
        }

        // Method to perform an exact search in the list of movies
        static void PerformExactSearch(List<string> movies, string exactSearch)
        {
            bool found = movies
                .Any(movie => movie.ToLower() == exactSearch);

            if (found)
            {
                Console.WriteLine($"\n'{exactSearch}' was found in your list of favorite movies.");
            }
            else
            {
                Console.WriteLine($"\n'{exactSearch}' was not found in your list of favorite movies.");
            }
        }
    }
}
