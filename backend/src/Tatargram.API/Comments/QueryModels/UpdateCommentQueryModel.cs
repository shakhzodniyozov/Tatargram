using Tatargram.QueryModels;

namespace Tatargram.Comments.QueryModels;
public class UpdateCommentQueryModel : UpdateBaseQueryModel
{
    public string? Text { get; set; }
}