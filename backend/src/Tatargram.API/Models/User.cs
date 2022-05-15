using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;


namespace Tatargram.Models;

public class User : IdentityUser<Guid>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    [Column("DateOfBirth", TypeName = "timestamp without time zone")]
    public DateTime DateOfBirth { get; set; }
    public string? ProfileImage { get; set; }
    public ICollection<UserFollower> Followers { get; set; } = new List<UserFollower>();
    public ICollection<UserFollowing> Followings { get; set; } = new List<UserFollowing>();
    public ICollection<Post> Posts { get; set; } = null!;
    public ICollection<LikedPost> LikedPosts { get; set; } = null!;
    public ICollection<Image> Photos { get; set; } = new List<Image>();
}