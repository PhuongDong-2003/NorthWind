using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JWTAuthentication.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace NorthWind.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
 
    
     private readonly IConfiguration _configuration;

    public AuthenticationController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] Users  users)
    {
        // Thực hiện xác thực người dùng (điều này có thể thay đổi tùy thuộc vào nhu cầu của bạn)
        if (users == null)
    {
        return BadRequest("Invalid user data");
    }
        if (IsValidUser(users))
        {
            var token = GenerateToken("admin");
            return Ok(new { token });
        }

        return Unauthorized();
    }

    private bool IsValidUser(Users users)
    {
        var validUser = _configuration.GetSection("Users")
                                    .Get<List<Users>>()
                                    .Any(u => u.Username == users.Username && u.Password == users.Password );
        return validUser;
    }

    private string GenerateToken(string role)
    {   
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:ValidIssuer"],
            audience: _configuration["Jwt:ValidAudience"],
            claims: new List<Claim> { new Claim(ClaimTypes.Role, role) },
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpiresInMinutes"])),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    }
}