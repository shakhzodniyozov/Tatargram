public class PostViewModel : PostBaseViewModel
{
    public string? AuthorUserName { get; set; }
    public string? AuthorFullName { get; set; }
    public string? Description { get; set; }
    public string? PublishDate { get; set; }
    public int Likes { get; set; }
    public bool Liked { get; set; }
    public List<string> Images { get; set; } = new();
    public string? AuthorPhoto { get; set; }
}