using BookStore.DTOs;
using BookStore.Exceptions;
using BookStore.Models;
using BookStore.Repositories;

namespace BookStore.Services;

public class BookService(IBookRepository bookRepository, IWebHostEnvironment webHostEnvironment) : IBookService
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

    public async Task<string> AddBookAsync(CreateBookDto book)
    {
        var _book = new Book();
        _book.Id = Guid.NewGuid().ToString();
        _book.Title = book.Title;
        _book.Price = book.Price;
        _book.FileName = "unavailable";
        _book.ImageName = "unavailable";
        _book.CategoryId = book.CategoryId;
        _book.Authors = book.Authors;
        await bookRepository.AddAsync(_book);
        return _book.Id;
    }

    public async Task UploadPdf(string id, IFormFile file)
    {
        var book = await GetBookByIdAsync(id);

        var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
        var uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "files");

        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        book.FileName = uniqueFileName;
        
        await bookRepository.UpdateAsync(book);
    }

    public async Task UploadImage(string id, IFormFile file)
    {
        var book = await GetBookByIdAsync(id);
        
        if (file == null || file.Length == 0)
            throw new ArgumentException("File is empty");

        var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
        var uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");

        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        book.ImageName = uniqueFileName;
        
        await bookRepository.UpdateAsync(book);
    }

    public async Task UpdateBookAsync(string id, UpdateBookDto book)
    {
        var _book = await GetBookByIdAsync(id);
        _book.Title = book.Title ?? _book.Title;
        _book.Price = book.Price.HasValue ? book.Price.Value : _book.Price;
        _book.CategoryId = book.CategoryId ?? _book.CategoryId;
        _book.Authors = book.Authors ?? _book.Authors;
        await bookRepository.UpdateAsync(_book);
    }

    public async Task DeleteBookAsync(string id)
    {
        await bookRepository.DeleteAsync(id);
    }
}