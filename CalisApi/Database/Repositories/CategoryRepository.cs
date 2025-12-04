using CalisApi.Database.Interfaces;
using CalisApi.Models;

namespace CalisApi.Database.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DatabaseContext _context;

        public CategoryRepository(DatabaseContext context)
        {
            _context = context;
        }
        
        public async Task<Category> AddCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            await  _context.SaveChangesAsync();
            return category;
        }
    }
}
