using Tatargram.Interfaces;

namespace Tatargram.Models;
public class Post : IEntity
{
    public Guid Id { get; set; }
    public DateTime PublishDate { get; set; } = DateTime.UtcNow;
    public string? Url { get; set; }
    public string? Description { get; set; }
    public int Likes { get; set; }
    public User Author { get; set; } = null!;
    public Guid AuthorId { get; set; }
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public ICollection<LikedPost> LikedUsers { get; set; } = new List<LikedPost>();
}