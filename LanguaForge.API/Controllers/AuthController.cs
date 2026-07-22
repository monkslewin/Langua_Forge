using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LanguaForge.API.Models;


namespace LanguaForge.API.Controllers;


[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{

    private readonly UserManager<ApplicationUser> _userManager;


    public AuthController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }



    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {

        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName
        };


        var result = await _userManager.CreateAsync(
            user,
            request.Password
        );


        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }


        return Ok(new
        {
            message = "User created successfully"
        });

    }



    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);


        if(user == null)
        {
            return Unauthorized();
        }


        var passwordCorrect =
            await _userManager.CheckPasswordAsync(
                user,
                request.Password
            );


        if(!passwordCorrect)
        {
            return Unauthorized();
        }


        return Ok(new
        {
            message = "Login successful",
            user.Id,
            user.Email
        });
    }

}

public class RegisterRequest
{
    public string FirstName { get; set; } = "";

    public string LastName { get; set; } = "";

    public string Email { get; set; } = "";

    public string Password { get; set; } = "";
}

public class LoginRequest
{
    public string Email { get; set; } = "";

    public string Password { get; set; } = "";
}