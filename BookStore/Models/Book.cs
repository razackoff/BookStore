namespace BookStore.Models;

public class Book
{
    public string Id { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
    public string FileName { get; set; }
    public string ImageName { get; set; }
    public string CategoryId { get; set; }
    public List<string> Authors { get; set; }
}