using BookStore.Models;

namespace BookStore.Services;

public interface IBookService
{
    Task<List<Book>> GetAllBooksAsync();
    Task<Book> GetBookByIdAsync(string id);
    Task AddBookAsync(Book book);
    Task UpdateBookAsync(Book book);
    Task DeleteBookAsync(string id);
}