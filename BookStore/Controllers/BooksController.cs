using BookStore.DTOs;
using BookStore.Exceptions;
using BookStore.Models;
using BookStore.Services;
using BookStore.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController(IBookService bookService) : ControllerBase
{
    [HttpGet("GetAllBooks")]
    public async Task<ActionResult<IEnumerable<Book>>> GetAllBooks()
    {
        try
        {
            var books = await bookService.GetAllBooksAsync();
            return Ok(books);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpGet("GetBookById")]
    public async Task<ActionResult<Book>> GetBookById(string id)
    {
        try
        {
            var book = await bookService.GetBookByIdAsync(id);
            return Ok(book);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpPost("AddBook")]
    public async Task<ActionResult<Book>> AddBook([FromBody] CreateBookDto book)
    {
        try
        {
            var id = await bookService.AddBookAsync(book);
            return Ok(id);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpPost("UploadPdf")]
    public async Task<IActionResult> UploadPdf(string id, IFormFile file)
    {
        try
        {
            if (!FileUtility.IsPDF(file))
            {
                return BadRequest("Invalid file format. Only PDF files are allowed.");
            }
            
            await bookService.UploadPdf(id, file);
            return Ok("PDF uploaded successfully");
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost("UploadImage")]
    public async Task<IActionResult> UploadImage(string id, IFormFile file)
    {
        try
        {
            if (!FileUtility.IsImage(file))
            {
                return BadRequest("File type is not supported");
            }
            
            await bookService.UploadImage(id, file);
            return Ok("Image uploaded successfully");
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpPut("UpdateBook")]
    public async Task<IActionResult> UpdateBook(string id, [FromBody] UpdateBookDto book)
    {
        try
        {
            await bookService.UpdateBookAsync(id, book);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpDelete("DeleteBook")]
    public async Task<IActionResult> DeleteBook(string id)
    {
        try
        {
            await bookService.DeleteBookAsync(id);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}