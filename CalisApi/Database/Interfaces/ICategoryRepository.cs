using CalisApi.Models;

namespace CalisApi.Database.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category> AddCategoryAsync(Category category);
    }
}
