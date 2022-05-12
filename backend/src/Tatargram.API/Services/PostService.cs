using AutoMapper;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tatargram.Helpers;
using Tatargram.Interfaces.Repositories;
using Tatargram.Interfaces.Services;
using Tatargram.Models;
using Tatargram.Posts.QueryModels;

namespace Tatargram.Services;

public class PostService : BaseService<Post, PostBaseQueryModel>, IPostService
{
    private readonly ImageService imageService;

    public PostService(IPostRepository postRepository,
                        IMapper mapper,
                        UserManager<User> userManager,
                        IHttpContextAccessor contextAccessor,
                        ImageService imageService)
        : base(postRepository, mapper, userManager, contextAccessor)
    {
        this.imageService = imageService;
    }

    public override async Task Create(PostBaseQueryModel model)
    {
        var currentUser = await userManager.FindByNameAsync(contextAccessor.HttpContext?.User.Identity?.Name);
        if (currentUser == null)
            throw new NotFoundException("User not found");

        model.AuthorId = currentUser.Id;
        var post = mapper.Map<Post>(model);
        post.Id = Guid.NewGuid();
        post.Photos = await imageService.SetImages(post, ((CreatePostQueryModel)model).Photos);

        await repository.Create(post);
    }

    public async Task<IEnumerable<PostViewModel>> GetPagedFeedList(int page = 1, int pageSize = 30)
    {
        var posts = await ((IPostRepository)repository).GetPagedFeedList(page, pageSize);

        var viewModels = new List<PostViewModel>();

        var currentUser = await userManager.Users.Include(x => x.LikedPosts)
                                    .FirstOrDefaultAsync(x => x.NormalizedUserName == contextAccessor.HttpContext!.User!.Identity!.Name!.ToUpper());

        foreach (var p in posts)
        {
            var vm = new PostViewModel();

            vm.Liked = currentUser!.LikedPosts.Intersect(p.LikedUsers) != null;
            vm.Description = p.Description;
            vm.PublishDate = p.PublishDate.Humanize();
            vm.Likes = p.LikedUsers.Count;
            vm.Id = p.Id;

            viewModels.Add(vm);
        }

        return viewModels;
    }

    public async Task LikeThePost(Guid id)
    {
        var currentUser = await userManager.FindByNameAsync(contextAccessor.HttpContext?.User.Identity?.Name);

        var post = await repository.GetById(id, x => x.LikedUsers);

        if (post == null)
            throw new NotFoundException("Post was not found");

        post.LikedUsers.Add(new()
        {
            UserId = currentUser.Id,
            PostId = post.Id
        });

        await repository.Update(post);
    }

    public override Task Delete(Guid id)
    {
        imageService.DeletePostImages(id);
        return base.Delete(id);
    }
}