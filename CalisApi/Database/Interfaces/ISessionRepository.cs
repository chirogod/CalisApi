using CalisApi.Models;
using CalisApi.Models.DTOs;

namespace CalisApi.Database.Interfaces
{
    public interface ISessionRepository
    {
        Task<IEnumerable<Session>> GetAll();
        Task<Session> GetSessionById(int id);
        Task<bool> VerifyEnroll(int userId, int sessionId);
        Task<Session> GetByDate(DateTime date);
        Task Create(Session session);
        Task Enroll(UserSession userSession);

        Task<List<SessionUserDataDto>> GetEnrolledUsers(int id);
    }
}
