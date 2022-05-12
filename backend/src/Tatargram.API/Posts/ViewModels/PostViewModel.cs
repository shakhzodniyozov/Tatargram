public class PostViewModel : PostBaseViewModel
{
    public Guid AuthorId { get; set; }
    public string? AuthorFullName { get; set; }
    public string? Description { get; set; }
    public string? PublishDate { get; set; }
    public int Likes { get; set; }
    public bool Liked { get; set; }
    public List<string> Images { get; set; } = new();
    public string? AuthorAvatarImage { get; set; }
}