using CalisApi.Models;

namespace CalisApi.Database.Interfaces
{
    public interface IUserRepository
    {

        Task<IEnumerable<User>> GetAll();
        Task<IEnumerable<User>> GetAllUsuarios();
        Task<User> GetById(int id);
        Task<User> GetUsuarioById(int id);
        Task<User> GetByEmail(string email);
        Task Add(User user);
        
    }
}
