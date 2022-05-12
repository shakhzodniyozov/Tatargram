namespace Tatargram.Models;

public class LikedPost
{
    public Guid Id { get; set; }
    public User User { get; set; } = null!;
    public Guid UserId { get; set; }
    public Post Post { get; set; } = null!;
    public Guid PostId { get; set; }
}