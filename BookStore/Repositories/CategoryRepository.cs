using BookStore.Data;
using BookStore.Exceptions;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repositories;

public class CategoryRepository(ApplicationDbContext dbContext): ICategoryRepository
{
    public async Task<List<Category>> GetAllAsync()
    {
        return await dbContext.Categories.ToListAsync();
    }

    public async Task<Category> GetByIdAsync(string id)
    {
        var category = await dbContext.Categories.FindAsync(id);
        if (category == null)
        {
            throw new NotFoundException($"Category with id {id} not found");
        }
        return category;
    }

    public async Task AddAsync(Category category)
    {
        dbContext.Categories.Add(category);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Category category)
    {
        dbContext.Categories.Update(category);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(string id)
    {
        var category = await dbContext.Categories.FindAsync(id);
        if (category != null)
        {
            dbContext.Categories.Remove(category);
            await dbContext.SaveChangesAsync();
        }
    }
}