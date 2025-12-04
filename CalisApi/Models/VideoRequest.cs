namespace CalisApi.Models
{
    public class VideoRequest
    {
        public IFormFile? File { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
    }
}
