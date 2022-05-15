namespace Tatargram.Comments.QueryModels;

public class CommentBaseQueryModel
{
    public Guid PostId { get; set; }
    public string? Text { get; set; }
    public DateTime PublishDate { get; set; } = DateTime.UtcNow;
}