namespace Tatargram.Users.ViewModels;

public class UserInfoViewModel
{
    public Guid Id { get; set; }
    public string? FullName { get; set; }
    public string? DateOfBirth { get; set; }
    public bool IsSubscribed { get; set; }
    public string? ProfileImage { get; set; }
    public IEnumerable<PostViewModel>? Posts { get; set; }
    public IEnumerable<UserShortInfo>? Followers { get; set; }
    public IEnumerable<UserShortInfo>? Followings { get; set; }
}

public record UserShortInfo(Guid Id, string FullName);