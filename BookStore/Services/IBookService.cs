using BookStore.DTOs;
using BookStore.Models;

namespace BookStore.Services;

public interface IBookService
{
    Task<List<Book>> GetAllBooksAsync();
    Task<Book> GetBookByIdAsync(string id);
    Task<string> AddBookAsync(CreateBookDto bookDto);
    Task UploadPdf(string id, IFormFile file);
    Task UploadImage(string id, IFormFile file);
    Task UpdateBookAsync(string id, UpdateBookDto bookDto);
    Task DeleteBookAsync(string id);
}