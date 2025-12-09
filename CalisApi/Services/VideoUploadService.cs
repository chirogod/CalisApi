using Amazon.S3;
using Amazon.S3.Model;
using CalisApi.Database.Interfaces;
using CalisApi.Models;
using CalisApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CalisApi.Services
{
    public class VideoUploadService : IVideoUploadService
    {
        private readonly IAmazonS3 _s3Client;
        private readonly IConfiguration _configuration;
        private readonly IVideoRepository _videoRepository;

        private readonly string _bucketName = "calisapp-exercises";
        private readonly string _region;

        public VideoUploadService(IAmazonS3 s3Client, IConfiguration configuration, IVideoRepository videoRepository)
        {
            _s3Client = s3Client;
            _configuration = configuration;
            _region = _configuration["AWS:Region"] ?? "us-east-2";

            _videoRepository = videoRepository;
        }

        public async Task<Video> UploadVideoAsync(IFormFile file, VideoRequest metadata)
        {
            if (file.Length == 0)
            {
                throw new ArgumentException("El archivo está vacío.");
            }

            var fileExtension = Path.GetExtension(file.FileName);
            var s3Key = $"videos/{Guid.NewGuid()}{fileExtension}";

            try
            {
                var putRequest = new PutObjectRequest
                {
                    BucketName = _bucketName,
                    Key = s3Key,
                    InputStream = file.OpenReadStream(),
                    ContentType = file.ContentType
                };

                await _s3Client.PutObjectAsync(putRequest);

                var videoUrl = $"https://{_bucketName}.s3.{_region}.amazonaws.com/{s3Key}";

                return new Video
                {
                    Title = metadata.Title,
                    Description = metadata.Description,
                    CategoryId = metadata.CategoryId,
                    Url = videoUrl
                };
            }
            catch (AmazonS3Exception ex)
            {
                throw new Exception($"Error de S3 al subir el video: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task DeleteVideoAsync(int videoId)
        {
            var video = await _videoRepository.GetVideoByIdAsync(videoId);
            if (video == null) {
                throw new ArgumentException("El archivo no existe.");
            }
            var s3Key = $"videos/{video.Url}";

            try
            {
                var deleteRequest = new DeleteObjectRequest
                {
                    BucketName = _bucketName,
                    Key = s3Key
                };

                await _s3Client.DeleteObjectAsync(deleteRequest);

             }
            catch (AmazonS3Exception ex)
            {
                throw new Exception($"Error de S3 al eliminar el video: {ex.Message}");
            }
        }


    }
}
