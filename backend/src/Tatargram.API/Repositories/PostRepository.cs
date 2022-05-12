using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tatargram.Data;
using Tatargram.Interfaces.Repositories;
using Tatargram.Models;

namespace Tatargram.Repositories;

public class PostRepository : BaseRepository<Post>, IPostRepository
{
    private readonly UserManager<User> userManager;
    private readonly IHttpContextAccessor contextAccessor;

    public PostRepository(ApplicationDbContext context, UserManager<User> userManager, IHttpContextAccessor contextAccessor)
        : base(context)
    {
        this.userManager = userManager;
        this.contextAccessor = contextAccessor;
    }

    public async Task<IEnumerable<Post>> GetPagedFeedList(int page = 1, int pageSize = 30)
    {
        var currentUser = await userManager.Users
                                            .Include(x => x.Subscriptions)
                                            .FirstOrDefaultAsync(x => x.NormalizedUserName == contextAccessor.HttpContext!.User.Identity!.Name!.ToUpper());
        if (currentUser == null)
            throw new NotFoundException("User not found");

        var userSubscriptions = await context.Set<Subscription>().Where(x => x.SubscriberId == currentUser.Id)
                                            .Select(x => x.UserId).ToListAsync();

        var posts = await entities.Where(x => userSubscriptions.Contains(x.AuthorId))
                                    .Include(x => x.LikedUsers)
                                    .OrderByDescending(x => x.PublishDate)
                                    .Skip(page * pageSize - pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();
        return posts;
    }
}