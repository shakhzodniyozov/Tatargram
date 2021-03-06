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

    public async Task<UserInfoViewModel> GetUserInfo(string userName)
    {
        var user = await userManager.Users.Include(x => x.Followers)
                                            .Include(x => x.Followings)
                                            .Include(x => x.Photos)
                                            .Include(x => x.Posts).ThenInclude(x => x.LikedUsers)
                                            .Include(x => x.Posts).ThenInclude(x => x.Photos)
                                            .AsNoTracking()
                                            .FirstOrDefaultAsync(x => x.NormalizedUserName == userName.ToUpper());

        if (user == null)
            throw new NotFoundException("User not found");

        var followersId = user.Followers.Select(x => x.FollowerId);
        var followingsId = user.Followings.Select(x => x.FollowingToId);

        var followers = await userManager.Users.Where(x => followersId.Contains(x.Id)).Select(x => new UserShortInfo(x.Id, $"{x.FirstName} {x.LastName}")).ToArrayAsync();
        var followings = await userManager.Users.Where(x => followingsId.Contains(x.Id)).Select(x => new UserShortInfo(x.Id, $"{x.FirstName} {x.LastName}")).ToArrayAsync();

        // var posts = user.Posts.OrderByDescending(x => x.PublishDate).Select(x => new PostViewModel()
        // {
        //     AuthorUserName = user.UserName,
        //     AuthorFullName = $"{user.FirstName} {user.LastName}",
        //     Description = x.Description,
        //     Images = x.Photos.Select(p => p.RelativePaths!).ToList(),
        //     PublishDate = x.PublishDate.Humanize(null, null, new CultureInfo("ru-RU")),
        //     Id = x.Id,
        //     Likes = x.LikedUsers.Count
        // });

        var posts = mapper.Map<IEnumerable<PostViewModel>>(user.Posts.OrderByDescending(x => x.PublishDate).ToArray());

        var viewModel = new UserInfoViewModel()
        {
            FullName = $"{user.FirstName} {user.LastName}",
            DateOfBirth = user.DateOfBirth.ToString("dd.MM.yyyy"),
            Followers = followers,
            Followings = followings,
            Posts = posts,
            IsSubscribed = user.Followings.Any(x => x.UserId == user.Id),
            ProfileImage = currentUser.ProfileImage,
            Id = currentUser.Id
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
                                    .Select(x => new UserSuggestionViewModel()
                                    {
                                        UserName = x.UserName,
                                        FullName = $"{x.FirstName} {x.LastName}",
                                        ProfileImage = x.ProfileImage
                                    }).ToArrayAsync();

        return users;
    }

    public async Task<object> SetProfileImage(SetProfileImageQueryModel model)
    {
        if (!string.IsNullOrEmpty(currentUser.ProfileImage))
            imageService.DeleteImage(currentUser.ProfileImage);

        if (!string.IsNullOrEmpty(model.ImagePath))
        {
            this.currentUser.ProfileImage = model.ImagePath;
        }
        else if (!string.IsNullOrEmpty(model.ImageAsBase64))
        {
            currentUser.ProfileImage = (await imageService.SetImages(currentUser, new string[] { model.ImageAsBase64 })).First().RelativePaths!;
        }

        await userManager.UpdateAsync(currentUser);

        return new { profileImage = currentUser.ProfileImage };
    }
}