using CalisApi.Models;

namespace CalisApi.Database.Interfaces
{
    public interface IVideoRepository
    {
        Task<IEnumerable<Video>> GetAllVideosAsync();
        Task<Video?> GetVideoByIdAsync(int id);
        Task<Video> CreateVideoAsync(Video video);
    }
}
