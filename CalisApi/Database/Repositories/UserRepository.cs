using CalisApi.Database.Interfaces;
using CalisApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CalisApi.Database.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;
        public UserRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<User> GetById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return null;
            }
            return user;
        }
        public async Task<User> GetUsuarioById(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(p=>p.Id == id && p.Role == "Usuario");
            if (user == null)
            {
                return null;
            }
            return user;
        }


        public async Task<User> GetByEmail(string email)
        {
            var user =  _context.Users.FirstOrDefault(x => x.Email == email);
            if (user == null) {
                return null;
            }
            return user;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<IEnumerable<User>> GetAllUsuarios()
        {
            return await _context.Users.Where(p=>p.Role == "Usuario").ToListAsync();
        }

        public async Task Add(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}
