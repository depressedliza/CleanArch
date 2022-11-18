using System.ComponentModel.DataAnnotations;

namespace Identity.DTOs;

public class LoginDto
{
    [MinLength(4)]
    public string Username { get; set; } = string.Empty;
    
    [MinLength(4)]
    public string Password { get; set; } = string.Empty;
}
