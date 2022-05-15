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
            User user1 = new()
            {
                UserName = "user1",
                FirstName = "Иван1",
                LastName = "Иванов1",
                DateOfBirth = DateTime.Now.AddYears(-20)
            };
            User user2 = new()
            {
                UserName = "user2",
                FirstName = "Иван2",
                LastName = "Иванов2",
                DateOfBirth = DateTime.Now.AddYears(-20)
            };
            userManager.CreateAsync(user1, "12345678").GetAwaiter().GetResult();
            userManager.CreateAsync(user2, "12345678").GetAwaiter().GetResult();
        }
        scope.Dispose();
    }
}