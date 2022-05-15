namespace Tatargram.API.Users.ViewModels;

public class CurrentUserPostViewModel
{
    public string? PublishDate { get; set; }
    public int Likes { get; set; }
    public string? Description { get; set; }
    public List<string> Images { get; set; } = new();
}