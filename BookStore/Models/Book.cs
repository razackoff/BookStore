namespace BookStore.Models;

public class Book
{
    public string Id { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
    public string FileId { get; set; }
    public string ImageId { get; set; }
    public string CategoryId { get; set; }
    public List<string> Authors { get; set; }
}