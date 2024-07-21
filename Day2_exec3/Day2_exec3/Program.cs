using System;

namespace ProcessTestScores
{
    class Program
    {
        static void Main(string[] args)
        {
            // Call GetTestScores method to get an array of test scores
            int[] testScores = GetTestScores();

            // Call GetHighestScore method to find the highest score
            int highestScore = GetHighestScore(testScores);

            // Call GetAverageScore method to calculate the average score
            double averageScore = GetAverageScore(testScores);

            // Call GetLowestScore method to find the lowest score
            int lowestScore = GetLowestScore(testScores);

            // Display the results
            Console.WriteLine($"Highest score: {highestScore}");
            Console.WriteLine($"Average score: {averageScore:F2}");
            Console.WriteLine($"Lowest score: {lowestScore}");
        }

        // Method to read and return an array of test scores
        static int[] GetTestScores()
        {
            int[] scores = new int[6]; // Assuming 6 test scores for this example

            Console.WriteLine("Enter 6 test scores:");

            for (int i = 0; i < scores.Length; i++)
            {
                Console.Write($"Score {i + 1}: ");
                scores[i] = Convert.ToInt32(Console.ReadLine());
            }

            return scores;
        }

        // Method to find and return the highest score in the array
        static int GetHighestScore(int[] scores)
        {
            int highest = scores[0];

            for (int i = 1; i < scores.Length; i++)
            {
                if (scores[i] > highest)
                {
                    highest = scores[i];
                }
            }

            return highest;
        }

        // Method to calculate and return the average score of the array
        static double GetAverageScore(int[] scores)
        {
            int sum = 0;

            foreach (int score in scores)
            {
                sum += score;
            }

            double average = (double)sum / scores.Length;
            return average;
        }

        // Method to find and return the lowest score in the array
        static int GetLowestScore(int[] scores)
        {
            int lowest = scores[0];

            for (int i = 1; i < scores.Length; i++)
            {
                if (scores[i] < lowest)
                {
                    lowest = scores[i];
                }
            }

            return lowest;
        }
    }
}
