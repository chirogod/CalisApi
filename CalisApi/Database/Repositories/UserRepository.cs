using CalisApi.Database.Interfaces;
using CalisApi.Models;

namespace CalisApi.Database.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;
        public UserRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<User> GetByEmail(string email)
        {
            var user =  _context.Users.FirstOrDefault(x => x.Email == email);
            if (user == null) {
                return null;
            }
            return user;
        }

        public async Task Add(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}
