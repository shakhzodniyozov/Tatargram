namespace Tatargram.Domain.Entities;
public class Post
{
    public Guid Id { get; set; }
    public DateTime PublishDate { get; set; }
    public int MyProperty { get; set; }
    public string? Url { get; set; }
    public string? Description { get; set; }
    public int Likes { get; set; }
    public Person Person { get; set; } = null!;
    public Guid PersonId { get; set; }
    public ICollection<Comment> Comments { get; set; } = null!;
}