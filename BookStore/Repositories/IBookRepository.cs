using BookStore.Models;

namespace BookStore.Repositories;

public interface IBookRepository
{
    Task<List<Book>> GetAllAsync();
    Task<Book> GetByIdAsync(string id);
    Task AddAsync(Book book);
    Task UpdateAsync(Book book);
    Task DeleteAsync(string id);
}