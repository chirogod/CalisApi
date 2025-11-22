using CalisApi.Models;

namespace CalisApi.Database.Interfaces
{
    public interface ISessionRepository
    {
        Task<Session> GetSessionById(int id);
        Task<bool> VerifyEnroll(int userId, int sessionId);
        Task Create(Session session);
        Task Enroll(UserSession userSession);
    }
}
