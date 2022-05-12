using Tatargram.Interfaces;

namespace Tatargram.Models;

public class Subscription : IEntity
{
    public Guid Id { get; set; }
    public User User { get; set; } = null!;
    public User Subscriber { get; set; } = null!;
    public Guid UserId { get; set; }
    public Guid SubscriberId { get; set; }
}