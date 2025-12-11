using CalisApi.Database.Interfaces;
using CalisApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CalisApi.Database.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DatabaseContext _context;

        public CategoryRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if(category == null)
            {
                return null;
            }
            return category;
        }


        public async Task<Category> AddCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            await  _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category> UpdateCategoryAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }
    }
}
