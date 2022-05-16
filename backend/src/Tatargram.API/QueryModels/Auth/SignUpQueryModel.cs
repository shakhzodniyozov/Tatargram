namespace Tatargram.QueryModels.Auth;

public class SignUpQueryModel
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? UserName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? Password { get; set; }
}