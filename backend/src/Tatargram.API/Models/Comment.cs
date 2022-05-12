namespace Tatargram.Models;
public class Comment
{
    public Guid Id { get; set; }
    public DateTime Posted { get; set; }
    public string? Text { get; set; }
    public bool IsResponse { get; set; }
    public Post Post { get; set; } = null!;
    public Guid PostId { get; set; }
}