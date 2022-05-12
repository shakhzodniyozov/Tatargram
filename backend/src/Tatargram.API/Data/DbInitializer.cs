using Microsoft.AspNetCore.Identity;
using Tatargram.Models;

namespace Tatargram.Data;

public class DbInitializer
{
    public static void Init(IServiceProvider services)
    {
        var scope = services.CreateScope();
        var ctx = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

        ctx.Database.EnsureCreated();

        if (userManager.FindByNameAsync("user1").GetAwaiter().GetResult() == null)
        {
            User user = new() { UserName = "user1" };
            userManager.CreateAsync(user, "12345678").GetAwaiter().GetResult();
        }
        scope.Dispose();
    }
}