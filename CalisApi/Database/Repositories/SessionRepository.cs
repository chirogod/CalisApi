using CalisApi.Database.Interfaces;
using CalisApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CalisApi.Database.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly DatabaseContext _context;
        public SessionRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Session>> GetAll()
        {
            return await _context.Sessions.ToListAsync();
        }
        public async Task<Session> GetSessionById(int id)
        {
            var ses = await _context.Sessions.FindAsync(id);
            if (ses == null) 
            {
                return null;
            }
            return ses;
        }
        public async Task<bool> VerifyEnroll(int userId, int sessionId)
        {
            var exist = await _context.UserSessions.AnyAsync(x => x.UserId== userId && x.SessionId== sessionId);

            return exist;
        }

        public async Task Create(Session session)
        {
            _context.Add(session);
            await _context.SaveChangesAsync();
        }

        public async Task<Session> GetByDate(DateTime date)
        {
            var e = await _context.Sessions.FirstOrDefaultAsync(x => x.Date == date);
            if(e == null)
            {
                return null;
            }
            return e;
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
            catch (Exception) {
                await transaction.RollbackAsync();
                throw;
            }
            
        }
    }
}
