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
using NorthWind.Api.Authentication;

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
        public IActionResult Login([FromBody] Users users)
        {
           
            if (users == null)
            {
                return BadRequest("Invalid user data");
            }
            var user = GetUserRole(users);

            if (user != null)
            {
                var token = GenerateToken(user.Role);
                return Ok(new { token });
            }

            return Unauthorized();
        }

        private UserRole GetUserRole(Users users)
        {
            return _configuration.GetSection("Users")
                                 .Get<List<UserRole>>()
                                 .FirstOrDefault(u => u.Username == users.Username && u.Password == users.Password);


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