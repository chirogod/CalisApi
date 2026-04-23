using CalisApi.Database.Interfaces;
using CalisApi.Models;
using CalisApi.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CalisApi.Database.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly DatabaseContext _context;
        public PostRepository(DatabaseContext context) {
            _context = context;
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await _context.Posts.ToListAsync();

        }
        public async Task<Post> GetByIdAsync(int id)
        {
            return await _context.Posts.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Post> Create(PostRequest request)
        {
            Post p = new Post
            {
                Title = request.Title,
                Content = request.Content,
                CreatedAt = DateTime.UtcNow
            };
            await _context.Posts.AddAsync(p);
            await _context.SaveChangesAsync();
            return p;
        }
    }
}
