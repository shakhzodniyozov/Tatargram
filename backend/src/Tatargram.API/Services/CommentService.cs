using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Tatargram.Interfaces.Repositories;
using Tatargram.Interfaces.Services;
using Tatargram.Models;

namespace Tatargram.Services;

public class CommentService : BaseService<Comment, CommentBaseQueryModel>, ICommentService
{
    public CommentService(ICommentRepository commentRepository,
                            IMapper mapper,
                            UserManager<User> userManager,
                            IHttpContextAccessor contextAccessor)
        : base(commentRepository, mapper, userManager, contextAccessor)
    {

    }
}