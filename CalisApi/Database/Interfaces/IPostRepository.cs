using CalisApi.Models;
using CalisApi.Models.DTOs;

namespace CalisApi.Database.Interfaces
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllAsync();
        Task<Post> GetByIdAsync(int id);
        Task<Post> Create(PostRequest request);

    }
}
