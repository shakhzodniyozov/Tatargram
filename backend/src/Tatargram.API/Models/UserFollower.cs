namespace Tatargram.Models;

public class UserFollower
{
    public Guid Id { get; set; }
    public User User { get; set; } = null!;
    public Guid UserId { get; set; }
    public User Follower { get; set; } = null!;
    public Guid FollowerId { get; set; }
}