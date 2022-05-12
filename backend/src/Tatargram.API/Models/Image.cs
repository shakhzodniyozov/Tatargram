namespace Tatargram.Models;

public class Image
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public User? User { get; set; }
    public Guid? UserId { get; set; }
    public Post? Post { get; set; }
    public Guid? PostId { get; set; }
}