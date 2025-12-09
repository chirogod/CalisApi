using CalisApi.Models;

namespace CalisApi.Services.Interfaces
{
    public interface IVideoUploadService
    {
        Task DeleteVideoAsync(int videoId);
        Task<Video> UploadVideoAsync(IFormFile file, VideoRequest metadata);
    }
}
