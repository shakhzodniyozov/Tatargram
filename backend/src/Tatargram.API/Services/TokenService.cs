using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Tatargram.Models;

namespace Tatargram.Services;

public class TokenService
{
    private readonly IConfiguration configuration;
    private readonly UserManager<User> userManager;

    public TokenService(IConfiguration configuration, UserManager<User> userManager)
    {
        this.configuration = configuration;
        this.userManager = userManager;
    }

    public string GenerateToken(User user)
    {
        var claims = new List<Claim>();
        claims.Add(new Claim(JwtRegisteredClaimNames.Name, user.UserName));

        byte[] secretKey = Encoding.ASCII.GetBytes(configuration["JWT:Secret"]);
        var securityKey = new SymmetricSecurityKey(secretKey);

        JwtSecurityTokenHandler handler = new();

        var token = new JwtSecurityToken(
            issuer: configuration["JWT:ValidIssuer"],
            audience: configuration["JWT:ValidAudience"],
            claims: claims,
            signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
        );

        return handler.WriteToken(token);
    }
}