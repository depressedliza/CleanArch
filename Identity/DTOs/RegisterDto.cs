using System.ComponentModel.DataAnnotations;

namespace Identity.DTOs;

public class RegisterDto
{
    [MinLength(4)]
    public string Username { get; set; } = string.Empty;
    
    [MinLength(4)]
    public string Password { get; set; } = string.Empty;
}
