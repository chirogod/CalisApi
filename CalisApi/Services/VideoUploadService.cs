using Amazon.S3;
using Amazon.S3.Model;
using CalisApi.Models;
using CalisApi.Services.Interfaces;

namespace CalisApi.Services
{
    public class VideoUploadService : IVideoUploadService
    {
        private readonly IAmazonS3 _s3Client;
        private readonly IConfiguration _configuration;
        private readonly string _bucketName = "calisapp-exercises";
        private readonly string _region;

        public VideoUploadService(IAmazonS3 s3Client, IConfiguration configuration)
        {
            _s3Client = s3Client;
            _configuration = configuration;
            _region = _configuration["AWS:Region"] ?? "us-east-2";
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


    }
}
