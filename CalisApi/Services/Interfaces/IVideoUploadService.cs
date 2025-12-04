using CalisApi.Models;

namespace CalisApi.Services.Interfaces
{
    public interface IVideoUploadService
    {
        Task<Video> UploadVideoAsync(IFormFile file, VideoRequest metadata);
    }
}
