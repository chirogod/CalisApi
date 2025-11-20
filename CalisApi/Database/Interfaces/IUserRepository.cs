using CalisApi.Models;

namespace CalisApi.Database.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByEmail(string email);
        Task Add(User user);
        
    }
}
