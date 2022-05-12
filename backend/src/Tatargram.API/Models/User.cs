using Microsoft.AspNetCore.Identity;

namespace Tatargram.Models;

public class User : IdentityUser<Guid>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int Age { get; set; }
    public string? ImageUrl { get; set; }
    public ICollection<Subscription> Subscriptions { get; set; } = null!;
    public ICollection<Post> Posts { get; set; } = null!;
}