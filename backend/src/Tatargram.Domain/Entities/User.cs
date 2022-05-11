namespace Tatargram.Domain.Entities;
public class User
{
    public Guid Id { get; set; }
    public string? ImageUrl { get; set; }
    public ICollection<Subscription> Subscriptions { get; set; } = null!;
    public ICollection<Post> Posts { get; set; } = null!;
}