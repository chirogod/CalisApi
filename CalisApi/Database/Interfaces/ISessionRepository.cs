using CalisApi.Models;
using CalisApi.Models.DTOs;

namespace CalisApi.Database.Interfaces
{
    public interface ISessionRepository
    {
        Task<IEnumerable<Session>> GetAll();
        Task<Session> GetSessionById(int id);
        
        Task<IEnumerable<Session>> GetAllSessionsByDate(DateTime date);

        Task<Session> GetSessionByDate(DateTime date);
        Task Create(Session session);
        
        Task<List<SessionUserDataDto>> GetEnrolledUsers(int id);

        Task<List<Achievement>> GetSessionAchievements(int sessionId);


    }
}
