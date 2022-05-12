namespace Tatargram.Models;

public class Image
{
    public Guid Id { get; set; }
    public string? AbsolutePath { get; set; }
    public string? RelativePaths
    {
        get
        {
            return AbsolutePath?.Substring(AbsolutePath!.IndexOf("wwwroot") + 7);
        }
    }
    public User? User { get; set; }
    public Guid? UserId { get; set; }
    public Post? Post { get; set; }
    public Guid? PostId { get; set; }
}