using BookStore.Data;
using BookStore.Exceptions;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repositories;

public class BookRepository(ApplicationDbContext dbContext) : IBookRepository
{
    public async Task<List<Book>> GetAllAsync()
    {
        return await dbContext.Books.ToListAsync();
    }

    public async Task<Book> GetByIdAsync(string id)
    {
        var book = await dbContext.Books.FindAsync(id);
        if (book == null)
        {
            throw new NotFoundException($"Book with id {id} not found");
        }
        return book;
    }

    public async Task AddAsync(Book book)
    {
        dbContext.Books.Add(book);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Book book)
    {
        dbContext.Books.Update(book);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(string id)
    {
        var book = await dbContext.Books.FindAsync(id);
        if (book != null)
        {
            dbContext.Books.Remove(book);
            await dbContext.SaveChangesAsync();
        }
    }
}