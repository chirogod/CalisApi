using CalisApi.Models;

namespace CalisApi.Database.Interfaces
{
    public interface IAchievementRepository
    {
        Task<IEnumerable<Achievement>> GetAllAchievements();    
        Task<Achievement> CreateAchievement(Achievement achievement);
        Task AssignAchievementsToUsers(List<UserAchievement> userAchievements);
    }
}
