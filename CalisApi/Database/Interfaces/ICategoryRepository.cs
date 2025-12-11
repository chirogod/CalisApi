using CalisApi.Models;

namespace CalisApi.Database.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryById(int id);
        Task<Category> AddCategoryAsync(Category category);

        Task<Category> UpdateCategoryAsync(Category category);
        Task DeleteCategory(int id);
    }
}
