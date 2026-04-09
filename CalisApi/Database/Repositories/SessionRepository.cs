using CalisApi.Database.Interfaces;
using CalisApi.Models;
using CalisApi.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

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
            return await _context.Sessions.Include(s => s.SessionAchievements).ToListAsync();
        }
        public async Task<Session> GetSessionById(int id)
        {
            return await _context.Sessions
        .Include(s => s.SessionAchievements)
        .FirstOrDefaultAsync(s => s.Id == id);
        }
        


        public async Task Create(Session session)
        {
            _context.Add(session);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Session>> GetAllSessionsByDate(DateTime date)
        {
            return await _context.Sessions
                                .Include(s => s.SessionAchievements)
                                .Where(x => x.Date.Date == date.Date)
                                .ToListAsync();
        }

        public async Task<Session> GetSessionByDate(DateTime date)
        {
            var e = await _context.Sessions.FirstOrDefaultAsync(x => x.Date == date);
            if (e == null)
            {
                return null;
            }
            return e;
        }

        public async Task<List<SessionUserDataDto>> GetEnrolledUsers(int id)
        {
            return await _context.UserSessions
                        .Where(us => us.SessionId == id)
                        .Include(us => us.User)
                        .Select(us => new SessionUserDataDto
                        {
                            Id = us.User.Id,
                            FullName = us.User.FullName
                        })
                        .ToListAsync();
        }

        public async Task<List<Achievement>> GetSessionAchievements(int sessionId)
        {
            return await _context.SessionAchievements
                        .Where(sa => sa.SessionId == sessionId)
                        .Include(sa => sa.Achievement)
                        .Select(sa => sa.Achievement)
                        .ToListAsync(); 
        }

    }
}
