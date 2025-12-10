using CalisApi.Database.Interfaces;
using CalisApi.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CalisApi.Database.Repositories
{
    public class VideoRepository : IVideoRepository
    {
        private readonly DatabaseContext _context;
        public VideoRepository(DatabaseContext databaseContext) { 
            _context = databaseContext;
        }
        public async Task<IEnumerable<Video>> GetAllVideosAsync(int? categoryId, string? searchTerm)
        {
            var query = _context.Videos.AsQueryable();

            query = query.Include(v => v.Category);

            if (categoryId.HasValue && categoryId.Value > 0)
            {
                query = query.Where(v => v.CategoryId == categoryId.Value);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                string searchLower = searchTerm.ToLower();

                query = query.Where(v => v.Title.ToLower().Contains(searchLower) || v.Description.ToLower().Contains(searchLower));
            }

            return await query.ToListAsync();
        }
        public async Task<Video?> GetVideoByIdAsync(int id)
        {
            return await _context.Videos.Include(v => v.Category).FirstOrDefaultAsync(v => v.Id == id);
        }
        public async Task<Video> CreateVideoAsync(Video video)
        {
            await _context.Videos.AddAsync(video);
            await _context.SaveChangesAsync();
            return video;
        }

        public async Task DeleteVideoAsync(int id)
        {
            var video = await GetVideoByIdAsync(id);
            if(video != null)
            {
                _context.Videos.Remove(video);
                await _context.SaveChangesAsync();
            }
        }
    }
}
