using CalisApi.Models;

namespace CalisApi.Database.Interfaces
{
    public interface IVideoRepository
    {
        Task<IEnumerable<Video>> GetAllVideosAsync(int? categoryId, string? searchTerm);
        Task<Video?> GetVideoByIdAsync(int id);
        Task<Video> CreateVideoAsync(Video video);

        Task DeleteVideoAsync(int id);
    }
}
