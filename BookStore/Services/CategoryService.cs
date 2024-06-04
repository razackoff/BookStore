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

    public async Task AddCategoryAsync(Category category)
    {
        await categoryRepository.AddAsync(category);
    }

    public async Task UpdateCategoryAsync(Category category)
    {
        await categoryRepository.UpdateAsync(category);
    }

    public async Task DeleteCategoryAsync(string id)
    {
        await categoryRepository.DeleteAsync(id);
    }
}