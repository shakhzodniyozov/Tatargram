using Tatargram.Interfaces;

namespace Tatargram.Models;
public class Comment : IEntity
{
    public Guid Id { get; set; }
    public DateTime PublishDate { get; set; }
    public string? Text { get; set; }
    public Post Post { get; set; } = null!;
    public Guid PostId { get; set; }
    public User User { get; set; } = null!;
    public Guid UserId { get; set; }
}