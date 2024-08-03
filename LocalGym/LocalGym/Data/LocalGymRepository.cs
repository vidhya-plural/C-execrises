using LocalGym.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocalGym.Data
{
    public class LocalGymRepository
    {
        private readonly ApplicationDbContext _context;

        public LocalGymRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<List<Member>> GetMembersAsync() => _context.Members.ToListAsync();
        public Task<Member> GetMemberAsync(int id) => _context.Members.FindAsync(id).AsTask();

        public Task<List<Trainer>> GetTrainersAsync() => _context.Trainers.ToListAsync();
        public Task<Trainer> GetTrainerAsync(int id) => _context.Trainers.FindAsync(id).AsTask();

        public Task<List<Session>> GetSessionsForTrainerAsync(int id) =>
            _context.Sessions.Include(s => s.Member).Where(s => s.TrainerId == id).ToListAsync();

        public Task<List<Session>> GetSessionsForMemberAsync(int id) =>
            _context.Sessions.Include(s => s.Trainer).Where(s => s.MemberId == id).ToListAsync();

        public async Task<Member> CreateMemberAsync(Member member)
        {
            _context.Members.Add(member);
            await _context.SaveChangesAsync();
            return member;
        }

        public async Task<Member> UpdateMemberAsync(Member member)
        {
            _context.Members.Update(member);
            await _context.SaveChangesAsync();
            return member;
        }

        public async Task<Trainer> CreateTrainerAsync(Trainer trainer)
        {
            _context.Trainers.Add(trainer);
            await _context.SaveChangesAsync();
            return trainer;
        }

        public async Task<Trainer> UpdateTrainerAsync(Trainer trainer)
        {
            _context.Trainers.Update(trainer);
            await _context.SaveChangesAsync();
            return trainer;
        }
    }
}
