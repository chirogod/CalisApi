using CalisApi.Models;

namespace CalisApi.Database.Interfaces
{
    public interface IUserSessionRepository
    {
        Task<bool> VerifyEnroll(int userId, int sessionId);
        Task Enroll(UserSession userSession);
        Task UnEnroll(UserSession userSession);
    }
}
