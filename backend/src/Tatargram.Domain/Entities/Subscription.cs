namespace Tatargram.Domain.Entities;

public class Subscription
{
    public Guid Id { get; set; }
    public User Person { get; set; } = null!;
    public User Subscriber { get; set; } = null!;
    public Guid UserId { get; set; }
    public Guid SubscriberId { get; set; }
}