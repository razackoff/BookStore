using BookStore.Models;

namespace BookStore.Services;

public interface ICategoryService
{
    Task<List<Category>> GetAllCategoriesAsync();
    Task<Category> GetCategoryByIdAsync(string id);
    Task<string> AddCategoryAsync(string category);
    Task UpdateCategoryAsync(string id, string category);
    Task DeleteCategoryAsync(string id);
}