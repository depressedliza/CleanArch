using Identity.DTOs;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<AppUser> _manager;
    private readonly SignInManager<AppUser> _sign;

    public AuthController(UserManager<AppUser> manager, SignInManager<AppUser> sign)
    {
        _manager = manager;
        _sign = sign;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var user = new AppUser
        {
            UserName = dto.Username
        };
        
        var result = await _manager.CreateAsync(user, dto.Password);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }
        
        await _sign.SignInAsync(user, false);
        return Ok();
    }
}
