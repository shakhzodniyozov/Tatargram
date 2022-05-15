namespace Tatargram.Models;

public class UserFollowing
{
    public Guid Id { get; set; }
    public User User { get; set; } = null!;
    public Guid UserId { get; set; }
    public User FollowingTo { get; set; } = null!;
    public Guid FollowingToId { get; set; }
}