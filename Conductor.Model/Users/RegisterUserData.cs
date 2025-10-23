using System.ComponentModel.DataAnnotations;

namespace Conductor.Models.Users;

public class RegisterUserData
{
    public required string Uid { get; set; }
    public required string Nickname { get; set; }
    public required string Email { get; set; }
    public required string Gender { get; set; }
    public required DateOnly DateOfBirth { get; set; }
}