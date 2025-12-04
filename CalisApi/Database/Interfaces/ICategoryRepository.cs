using CalisApi.Models;

namespace CalisApi.Database.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> AddCategoryAsync(Category category);
    }
}
