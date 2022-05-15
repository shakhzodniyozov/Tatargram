using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Tatargram.Comments.QueryModels;
using Tatargram.Interfaces.Repositories;
using Tatargram.Interfaces.Services;
using Tatargram.Models;

namespace Tatargram.Services;

public class CommentService : BaseService<Comment, CommentBaseQueryModel>, ICommentService
{
    private readonly User currentUser;

    public CommentService(ICommentRepository commentRepository,
                            IMapper mapper,
                            UserManager<User> userManager,
                            IHttpContextAccessor contextAccessor)
        : base(commentRepository, mapper, userManager, contextAccessor)
    {
        this.currentUser = userManager.FindByNameAsync(contextAccessor.HttpContext!.User.Identity!.Name).GetAwaiter().GetResult();
    }

    public override async Task Create(CommentBaseQueryModel model)
    {
        var comment = mapper.Map<Comment>(model);
        comment.UserId = currentUser.Id;

        await repository.Create(comment);
    }

    public override async Task Delete(Guid id)
    {
        var comment = await repository.GetById(id);

        if (comment != null && comment.UserId == currentUser.Id)
            await base.Delete(id);
    }
}