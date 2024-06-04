using BookStore.Exceptions;
using BookStore.Models;
using BookStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers;

[Route("api/categories")]
[ApiController]
public class CategoryController(ICategoryService categoryService) : ControllerBase
{
    [HttpGet("GetAllCategories")]
    public async Task<IActionResult> GetAllCategories()
    {
        var categories = await categoryService.GetAllCategoriesAsync();
        return Ok(categories);
    }

    [HttpGet("GetCategoryById")]
    public async Task<IActionResult> GetCategoryById(string id)
    {
        try
        {
            var category = await categoryService.GetCategoryByIdAsync(id);
            return Ok(category);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost("AddCategory")]
    public async Task<IActionResult> AddCategory(string category)
    {
        var id = await categoryService.AddCategoryAsync(category);
        return Ok(id);
    }

    [HttpPut("UpdateCategory")]
    public async Task<IActionResult> UpdateCategory(string id, string category)
    {
        try
        {
            await categoryService.UpdateCategoryAsync(id, category);
            return Ok(category);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("DeleteCategory")]
    public async Task<IActionResult> DeleteCategory(string id)
    {
        await categoryService.DeleteCategoryAsync(id);
        return NoContent();
    }
}