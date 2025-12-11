using CalisApi.Database.Interfaces;
using CalisApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CalisApi.Database.Repositories
{
    public class UserSessionRepository : IUserSessionRepository
    {
        private readonly DatabaseContext _context;
        public UserSessionRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<bool> VerifyEnroll(int userId, int sessionId)
        {
            var exist = await _context.UserSessions.AnyAsync(x => x.UserId == userId && x.SessionId == sessionId);

            return exist;
        }

        public async Task Enroll(UserSession userSession)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var sessionId = userSession.SessionId;
                var session = await _context.Sessions.FirstOrDefaultAsync(s => s.Id == sessionId);
                _context.UserSessions.Add(userSession);
                
                session.Enrolled += 1;
                _context.Sessions.Update(session);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }

        }

        public async Task UnEnroll(UserSession userSession)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var sessionId = userSession.SessionId;
                var userSessionToDelete = await _context.UserSessions.FirstOrDefaultAsync(s => s.UserId == userSession.UserId && s.SessionId == userSession.SessionId);
                if (userSessionToDelete == null)
                {
                    throw new InvalidOperationException("La inscripción no existe para ser eliminada.");
                }
                var session = await _context.Sessions.FirstOrDefaultAsync(s => s.Id == sessionId);
                
                _context.UserSessions.Remove(userSessionToDelete);
                session.Enrolled -= 1;
                _context.Sessions.Update(session);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }

        }
    }
}
