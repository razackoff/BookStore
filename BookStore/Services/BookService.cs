using BookStore.Exceptions;
using BookStore.Models;
using BookStore.Repositories;

namespace BookStore.Services;

public class BookService(IBookRepository bookRepository) : IBookService
{
    public async Task<List<Book>> GetAllBooksAsync()
    {
        return await bookRepository.GetAllAsync();
    }

    public async Task<Book> GetBookByIdAsync(string id)
    {
        var book = await bookRepository.GetByIdAsync(id);
        if (book == null)
        {
            throw new NotFoundException($"Book with id {id} not found");
        }
        return book;
    }

    public async Task AddBookAsync(Book book)
    {
        await bookRepository.AddAsync(book);
    }

    public async Task UpdateBookAsync(Book book)
    {
        await bookRepository.UpdateAsync(book);
    }

    public async Task DeleteBookAsync(string id)
    {
        await bookRepository.DeleteAsync(id);
    }
}