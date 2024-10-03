using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SimpleApi.Models;
using System.Security.Claims;

namespace SimpleApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController(SimpleApiContext context) : ControllerBase
{
    private readonly SimpleApiContext _context = context;

    [HttpPost("login")]
    public IActionResult Login([FromBody] Login user)
    {
        if (user == null)
        {
            return BadRequest("Invalid user request");
        }

        var loginUser = _context.Users.FirstOrDefault(d => d.UserName == user.UserName);

        if (loginUser != null && string.Compare(loginUser.Password, user.Password) == 0)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.AppSetting["JWT:Secret"] ?? string.Empty));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(issuer: ConfigurationManager.AppSetting["JWT:ValidIssuer"], audience: ConfigurationManager.AppSetting["JWT:ValidAudience"], claims: new List < Claim > (), expires: DateTime.Now.AddMinutes(6), signingCredentials: signinCredentials);
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new JWTTokenResponse {
                    Token = tokenString,
                    UserName = loginUser.FullName
                });
        }

        return Unauthorized();
    }
}