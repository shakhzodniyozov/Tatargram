using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Tatargram.Interfaces;

namespace Tatargram.Models;

public class User : IdentityUser<Guid>, IEntity
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    [Column("DateOfBirth", TypeName = "timestamp without time zone")]
    public DateTime DateOfBirth { get; set; }
    public string? ProfileImage { get; set; }
    public ICollection<UserFollower> Followers { get; set; } = new List<UserFollower>();
    public ICollection<UserFollowing> Followings { get; set; } = new List<UserFollowing>();
    public ICollection<Post> Posts { get; set; } = new List<Post>();
    public ICollection<LikedPost> LikedPosts { get; set; } = new List<LikedPost>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public ICollection<Image> Photos { get; set; } = new List<Image>();
}