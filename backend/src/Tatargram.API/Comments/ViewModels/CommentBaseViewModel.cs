namespace Tatargram.Comments.ViewModels;
public class CommentBaseViewModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string? UserFullName { get; set; }
    public string? UserProfileImage { get; set; }
    public string? PublishDate { get; set; }
}