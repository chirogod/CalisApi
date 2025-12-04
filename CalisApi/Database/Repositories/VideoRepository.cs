using CalisApi.Database.Interfaces;
using CalisApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CalisApi.Database.Repositories
{
    public class VideoRepository : IVideoRepository
    {
        private readonly DatabaseContext _context;
        public VideoRepository(DatabaseContext databaseContext) { 
            _context = databaseContext;
        }
        public async Task<IEnumerable<Video>> GetAllVideosAsync()
        {
            return await _context.Videos.ToListAsync();
        }
        public async Task<Video?> GetVideoByIdAsync(int id)
        {
            return await _context.Videos.FindAsync(id);
        }
        public async Task<Video> CreateVideoAsync(Video video)
        {
            await _context.Videos.AddAsync(video);
            await _context.SaveChangesAsync();
            return video;
        }
    }
}
