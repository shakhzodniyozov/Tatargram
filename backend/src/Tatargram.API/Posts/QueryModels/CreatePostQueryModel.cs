namespace Tatargram.Posts.QueryModels;

public class CreatePostQueryModel : PostBaseQueryModel
{
    public string? Description { get; set; }
    public string[] Photos { get; set; } = null!;
}