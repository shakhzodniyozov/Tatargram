using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tatargram.Models;
using Tatargram.QueryModels.Auth;
using Tatargram.Services;

namespace Tatargram.Contollers;

[ApiController, Route("api/[controller]")]
public class AccountController : Controller
{
    private readonly UserManager<User> userManager;
    private readonly TokenService tokenService;

    public AccountController(UserManager<User> userManager, TokenService tokenService)
    {
        this.userManager = userManager;
        this.tokenService = tokenService;
    }

    #region POST
    [HttpPost("signin")]
    public async Task<ActionResult<object>> SignIn(SignInQueryModel model)
    {
        var user = await userManager.FindByNameAsync(model.UserName!);

        if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
        {
            var token = tokenService.GenerateToken(user);

            return Ok(new { AccessToken = token });
        }

        return Unauthorized(new { Error = "Неправильный логин или пароль" });
    }

    [HttpPost("signup")]
    public async Task<ActionResult<object>> SignUp(SignUpQueryModel model)
    {
        if ((await userManager.FindByNameAsync(model.UserName)) == null)
        {
            var user = new User()
            {
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var createResult = await userManager.CreateAsync(user, model.Password);

            if (!createResult.Succeeded)
                throw new Exception("Что-то пошло не так. Повторите позже");

            return new
            {
                AccessToken = tokenService.GenerateToken(user)
            };
        }

        return StatusCode(409, new { Message = "Пользователь с таким именем уже существует" });
    }

    #endregion
}