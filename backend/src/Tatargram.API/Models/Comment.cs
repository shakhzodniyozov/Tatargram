using Tatargram.Interfaces;

namespace Tatargram.Models;
public class Comment : IEntity
{
    public DateTime Posted { get; set; }
    public string? Text { get; set; }
    public bool IsResponse { get; set; }
    public Post Post { get; set; } = null!;
    public Guid PostId { get; set; }
    public Guid Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
}