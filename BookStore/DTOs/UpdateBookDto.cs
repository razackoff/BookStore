namespace BookStore.DTOs;

public class UpdateBookDto
{
    public string? Title { get; set; }
    public decimal? Price { get; set; }
    public string? CategoryId { get; set; }
    public List<string>? Authors { get; set; }
}