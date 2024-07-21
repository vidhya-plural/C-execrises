using System;

namespace ClassPlay
{
    class SportsPlayer
    {
        // Properties
        public string Name { get; set; }
        public string Sport { get; set; }
        public int YearsExperience { get; set; }
        public string Team { get; set; }
        public bool IsProfessional { get; set; }

        // Constructor
        public SportsPlayer(string name, string sport, int yearsExperience, string team, bool isProfessional)
        {
            Name = name;
            Sport = sport;
            YearsExperience = yearsExperience;
            Team = team;
            IsProfessional = isProfessional;
        }

        // Method to print information about the sports player
        public void PrintPlayerInfo()
        {
            Console.WriteLine($"Player Name: {Name}");
            Console.WriteLine($"Sport: {Sport}");
            Console.WriteLine($"Years of Experience: {YearsExperience}");
            Console.WriteLine($"Team: {Team}");
            Console.WriteLine($"Professional: {(IsProfessional ? "Yes" : "No")}");
            Console.WriteLine();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create instances of SportsPlayer using the constructor
            SportsPlayer player1 = new SportsPlayer("Michael Jordan", "Basketball", 20, "Chicago Bulls", true);
            SportsPlayer player2 = new SportsPlayer("Serena Williams", "Tennis", 25, "USA", true);
            SportsPlayer player3 = new SportsPlayer("Tom Brady", "Football", 22, "Tampa Bay Buccaneers", true);

            // Call the method to print player information for each player
            player1.PrintPlayerInfo();
            player2.PrintPlayerInfo();
            player3.PrintPlayerInfo();
        }
    }
}
