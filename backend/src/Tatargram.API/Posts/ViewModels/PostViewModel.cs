public class PostViewModel : PostBaseViewModel
{
    public string? Description { get; set; }
    public string? PublishDate { get; set; }
    public int Likes { get; set; }
    public bool Liked { get; set; }
}