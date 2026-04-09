using CalisApi.Database.Interfaces;
using CalisApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CalisApi.Database.Repositories
{
    public class AchievementRepository : IAchievementRepository
    {
        private readonly DatabaseContext _context;
        public AchievementRepository(DatabaseContext context)
        { 
            _context = context;
        }

        public async Task<IEnumerable<Achievement>> GetAllAchievements()
        {
            return await _context.Achievements.ToListAsync();
        }
        public async Task<Achievement> CreateAchievement(Achievement achievement)
        {
            _context.AddAsync(achievement);
            await _context.SaveChangesAsync();
            return achievement;
        }

        public async Task AssignAchievementsToUsers(List<UserAchievement> userAchievements)
        {
            await _context.UserAchievements.AddRangeAsync(userAchievements);
            await _context.SaveChangesAsync();
        }
    }
}
