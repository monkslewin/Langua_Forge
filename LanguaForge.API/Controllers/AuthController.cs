using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LanguaForge.API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LanguaForge.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;

    public AuthController(
        UserManager<ApplicationUser> userManager,
        IConfiguration configuration
    )
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {

        var existingUser = await _userManager.FindByEmailAsync(request.Email);

        if(existingUser != null)
        {
            return BadRequest(new
            {
                message = "Email already registered"
            });
        }

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


        if(!result.Succeeded)
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

        var user = await _userManager.FindByEmailAsync(
            request.Email
        );

        if(user == null)
        {
            return Unauthorized(new
            {
                message = "Invalid email or password"
            });
        }

        var passwordCorrect =
            await _userManager.CheckPasswordAsync(
                user,
                request.Password
            );

        if(!passwordCorrect)
        {
            return Unauthorized(new
            {
                message = "Invalid email or password"
            });
        }

        var token = CreateToken(user);

        return Ok(new
        {
            message = "Login successful",

            token,

            user = new
            {
                user.Id,
                user.Email,
                user.FirstName,
                user.LastName
            }
        });

    }

    private string CreateToken(ApplicationUser user)
    {

        var claims = new[]
        {

            new Claim(
                JwtRegisteredClaimNames.Sub,
                user.Id
            ),

            new Claim(
                JwtRegisteredClaimNames.Email,
                user.Email!
            ),

            new Claim(
                "firstName",
                user.FirstName
            ),

            new Claim(
                "lastName",
                user.LastName
            )

        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                _configuration["Jwt:Key"]!
            )
        );

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256
        );

        var token = new JwtSecurityToken(

            issuer:
                _configuration["Jwt:Issuer"],


            audience:
                _configuration["Jwt:Audience"],

            claims: claims,

            expires:
                DateTime.UtcNow.AddHours(1),

            signingCredentials:
                credentials

        );

        return new JwtSecurityTokenHandler()
            .WriteToken(token);

    }

}




