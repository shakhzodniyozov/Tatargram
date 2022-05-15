using System.Text.Json.Serialization;
using Tatargram.JsonConverters;
using Tatargram.QueryModels;

namespace Tatargram.API.Users.QueryModels;

public class UpdateUserInfoQueryModel : UpdateBaseQueryModel
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime DateOfBirth { get; set; }
}