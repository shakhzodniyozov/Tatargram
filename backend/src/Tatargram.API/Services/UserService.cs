using System.Globalization;
using AutoMapper;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tatargram.API.Users.QueryModels;
using Tatargram.API.Users.ViewModels;
using Tatargram.Helpers;
using Tatargram.Interfaces;
using Tatargram.Interfaces.Services;
using Tatargram.Models;
using Tatargram.Users.QueryModels;
using Tatargram.Users.ViewModels;

namespace Tatargram.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> userManager;
    private readonly IHttpContextAccessor httpAccessor;
    private readonly IMapper mapper;
    private readonly ImageService imageService;
    private readonly User currentUser;

    public UserService(UserManager<User> userManager,
                        IMapper mapper,
                        IHttpContextAccessor httpAccessor,
                        ImageService imageService)
    {
        this.userManager = userManager;
        this.httpAccessor = httpAccessor;
        this.mapper = mapper;
        this.imageService = imageService;
        this.currentUser = userManager.FindByNameAsync(httpAccessor.HttpContext!.User.Identity!.Name).GetAwaiter().GetResult();
    }

    public async Task<UserInfoViewModel> GetCurrentUserInfo()
    {
        return await GetUserInfo(currentUser.Id);

    }

    public async Task<UserInfoViewModel> GetUserInfo(Guid id)
    {
        var user = await userManager.Users.Include(x => x.Followers)
                                            .Include(x => x.Followings)
                                            .Include(x => x.Photos)
                                            .Include(x => x.Posts).ThenInclude(x => x.LikedUsers)
                                            .Include(x => x.Posts).ThenInclude(x => x.Photos)
                                            .AsNoTracking()
                                            .FirstOrDefaultAsync(x => x.Id == id);

        if (user == null)
            throw new NotFoundException("User not found");

        var followersId = user.Followers.Select(x => x.FollowerId);
        var followingsId = user.Followings.Select(x => x.FollowingToId);

        var followers = await userManager.Users.Where(x => followersId.Contains(x.Id)).Select(x => new UserShortInfo(x.Id, $"{x.FirstName} {x.LastName}")).ToArrayAsync();
        var followings = await userManager.Users.Where(x => followingsId.Contains(x.Id)).Select(x => new UserShortInfo(x.Id, $"{x.FirstName} {x.LastName}")).ToArrayAsync();

        var posts = user.Posts.Select(x => new PostViewModel()
        {
            AuthorId = user.Id,
            AuthorFullName = $"{user.FirstName} {user.LastName}",
            Description = x.Description,
            Images = x.Photos.Select(p => p.RelativePaths!).ToList(),
            PublishDate = x.PublishDate.Humanize(null, null, new CultureInfo("ru-RU")),
            Id = x.Id,
            Likes = x.LikedUsers.Count
        });

        var viewModel = new UserInfoViewModel()
        {
            FullName = $"{user.FirstName} {user.LastName}",
            DateOfBirth = user.DateOfBirth.ToString("dd.MM.yyyy"),
            Followers = followers,
            Followings = followings,
            Posts = posts,
            IsSubscribed = user.Followings.Any(x => x.UserId == id),
            ProfileImage = currentUser.ProfileImage
        };

        return viewModel;
    }

    public async Task FollowTo(Guid id)
    {
        var currentUser = await userManager.Users.Include(x => x.Followings)
                                            .FirstAsync(x => x.NormalizedUserName == httpAccessor.HttpContext!.User.Identity!.Name!.ToUpper());

        var user = await userManager.Users.Include(x => x.Followers)
                                            .FirstAsync(x => x.Id == id);

        if (!currentUser.Followings.Any(x => x.FollowingToId == id))
            currentUser.Followings.Add(new() { UserId = currentUser.Id, FollowingToId = id });

        user.Followers.Add(new() { UserId = user.Id, FollowerId = currentUser.Id });

        await userManager.UpdateAsync(currentUser);
        await userManager.UpdateAsync(user);
    }

    public async Task UnfollowFrom(Guid id)
    {
        var currentUser = await userManager.Users.Include(x => x.Followings)
                                    .FirstAsync(x => x.NormalizedUserName == httpAccessor.HttpContext!.User.Identity!.Name!.ToUpper());

        var user = await userManager.Users.Include(x => x.Followers)
                                            .FirstAsync(x => x.Id == id);

        var following = currentUser.Followings.FirstOrDefault(x => x.FollowingToId == id);
        if (following != null)
            currentUser.Followings.Remove(following);

        user.Followers.Remove(user.Followers.Where(x => x.FollowerId == currentUser.Id).First());

        await userManager.UpdateAsync(currentUser);
        await userManager.UpdateAsync(user);
    }

    public async Task UpdateUser(UpdateUserInfoQueryModel model)
    {
        currentUser.FirstName = model.FirstName;
        currentUser.LastName = model.LastName;
        currentUser.DateOfBirth = model.DateOfBirth;

        await userManager.UpdateAsync(currentUser);
    }

    public async Task<IEnumerable<UserSuggestionViewModel>> Search(string value)
    {
        var users = await userManager.Users.AsNoTracking()
                                    .Where(x => x.FirstName!.ToUpper().StartsWith(value.ToUpper())
                                            || x.LastName!.ToUpper().StartsWith(value.ToUpper()))
                                    .Select(x => new UserSuggestionViewModel() { Id = x.Id, FullName = $"{x.FirstName} {x.LastName}" })
                                    .ToArrayAsync();

        return users;
    }

    public async Task SetProfileImage(SetProfileImageQueryModel model)
    {
        if (!string.IsNullOrEmpty(currentUser.ProfileImage))
            imageService.DeleteImage(currentUser.ProfileImage);

        if (model.ImagePath != null)
            this.currentUser.ProfileImage = model.ImagePath;
        else if (model.ImageAsBase64 != null)
            await imageService.SetImages((IEntity)currentUser, new string[] { model.ImageAsBase64 });

        await userManager.UpdateAsync(currentUser);
    }
}