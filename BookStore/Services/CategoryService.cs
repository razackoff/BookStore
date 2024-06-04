using BookStore.Exceptions;
using BookStore.Models;
using BookStore.Repositories;

namespace BookStore.Services;

public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
{
    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        return await categoryRepository.GetAllAsync();
    }

    public async Task<Category> GetCategoryByIdAsync(string id)
    {
        var category = await categoryRepository.GetByIdAsync(id);
        if (category == null)
        {
            throw new NotFoundException($"Category with id {id} not found");
        }
        return category;
    }

    public async Task<string> AddCategoryAsync(string category)
    {
        var _category = new Category();
        _category.Id = Guid.NewGuid().ToString();
        await categoryRepository.AddAsync(_category);
        return _category.Id;
    }

    public async Task UpdateCategoryAsync(string id, string category)
    {
        var _category = await GetCategoryByIdAsync(id);
        _category.Name = category;
        await categoryRepository.UpdateAsync(_category);
    }

    public async Task DeleteCategoryAsync(string id)
    {
        await categoryRepository.DeleteAsync(id);
    }
}