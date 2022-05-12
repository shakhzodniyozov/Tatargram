using Tatargram.Data;
using Tatargram.Interfaces.Repositories;
using Tatargram.Models;

namespace Tatargram.Repositories;

public class CommentRepository : BaseRepository<Comment>, ICommentRepository
{
    public CommentRepository(ApplicationDbContext context) : base(context)
    {

    }
}