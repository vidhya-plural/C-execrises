// Data/SeedData.cs
using LocalGym.Models;
using System;
using System.Linq;

namespace LocalGym.Data
{
    public static class SeedData
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // Check if the database has already been seeded
            if (context.Members.Any() || context.Trainers.Any() || context.Sessions.Any())
            {
                return; // DB has been seeded
            }

            // Seed Members
            var members = new[]
            {
                new Member { FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", JoinDate = DateTime.UtcNow },
                new Member { FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", JoinDate = DateTime.UtcNow }
            };
            context.Members.AddRange(members);
            context.SaveChanges(); // Save changes to generate IDs for foreign key references

            // Seed Trainers
            var trainers = new[]
            {
                new Trainer { FirstName = "Alan", LastName = "Turing", Speciality = "Programming", FeePer30Minutes = 30.00m, HireDate = DateTime.UtcNow },
                new Trainer { FirstName = "Grace", LastName = "Hopper", Speciality = "Mathematics", FeePer30Minutes = 45.00m, HireDate = DateTime.UtcNow }
            };
            context.Trainers.AddRange(trainers);
            context.SaveChanges(); // Save changes to generate IDs for foreign key references

            // Seed Sessions
            var sessions = new[]
            {
                new Session { MemberId = members[0].MemberId, TrainerId = trainers[0].TrainerId, SessionDate = DateTime.UtcNow, Duration = TimeSpan.FromMinutes(30) },
                new Session { MemberId = members[1].MemberId, TrainerId = trainers[1].TrainerId, SessionDate = DateTime.UtcNow, Duration = TimeSpan.FromMinutes(45) }
            };
            context.Sessions.AddRange(sessions);
            context.SaveChanges(); // Save changes to complete seeding
        }
    }
}
