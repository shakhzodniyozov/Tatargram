using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tatargram.API.Users.QueryModels;
using Tatargram.API.Users.ViewModels;
using Tatargram.Interfaces.Services;
using Tatargram.Users.QueryModels;
using Tatargram.Users.ViewModels;

namespace Tatargram.Contollers;

[Authorize, ApiController, Route("api/[controller]")]
public class UserController : Controller
{
    private readonly IUserService userService;

    public UserController(IUserService userService)
    {
        this.userService = userService;
    }

    #region GET

    [HttpGet("{userName}")]
    public async Task<ActionResult<UserInfoViewModel>> GetUserInfo(string userName)
    {
        return Ok(await userService.GetUserInfo(userName));
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<UserSuggestionViewModel>>> SearchUser(string value)
    {
        var users = await userService.Search(value);
        return Ok(users);
    }
    #endregion

    #region POST

    [HttpPost("follow/{userId}")]
    public async Task<IActionResult> SubscribeTo(Guid userId)
    {
        await userService.FollowTo(userId);
        return NoContent();
    }

    [HttpPost("unfollow/{userId}")]
    public async Task<IActionResult> UnfollowFrom(Guid userId)
    {
        await userService.UnfollowFrom(userId);
        return NoContent();
    }


    [HttpPost("profile-image")]
    public async Task<ActionResult<object>> SetProfileImage(SetProfileImageQueryModel model)
    {
        return Ok(await userService.SetProfileImage(model));
    }

    #endregion

    #region PUT

    [HttpPut]
    public async Task<IActionResult> UpdateUserInfo(UpdateUserInfoQueryModel model)
    {
        await userService.UpdateUser(model);
        return NoContent();
    }

    #endregion
}