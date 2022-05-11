using Microsoft.AspNetCore.Identity;
using Tatargram.Domain.Entities;

namespace Tatargram.Infrastructure.Identity;
public class ApplicationUser : IdentityUser<Guid>
{
    public User User { get; set; } = null!;
}