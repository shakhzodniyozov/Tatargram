using Tatargram.API.Users.QueryModels;
using Tatargram.API.Users.ViewModels;
using Tatargram.Users.QueryModels;
using Tatargram.Users.ViewModels;

namespace Tatargram.Interfaces.Services;

public interface IUserService
{
    Task<UserInfoViewModel> GetUserInfo(string userName);
    Task FollowTo(Guid userId);
    Task UnfollowFrom(Guid userId);
    Task UpdateUser(UpdateUserInfoQueryModel model);
    Task<IEnumerable<UserSuggestionViewModel>> Search(string value);
    Task SetProfileImage(SetProfileImageQueryModel model);
}