using Tatargram.QueryModels;

namespace Tatargram.Posts.QueryModels;

public class UpdatePostQueryModel : UpdateBaseQueryModel
{
    public string? Description { get; set; }
}