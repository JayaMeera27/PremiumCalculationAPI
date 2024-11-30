using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SolildPrinciples.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SolildPrinciples.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class LoginController : ControllerBase
    {
        private readonly string _secretKey = "YourLong256BitSecretKeyHere!SecretKeyHere"; // This should match the key used in token validation

        [HttpPost("login")]
        public IActionResult Login([FromBody] Login login)
        {
            // Validate user credentials (e.g., check against a database)
            if (login.Username == "test" && login.Password == "password") // Replace with actual validation
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_secretKey);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, login.Username)
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    Audience = "YourAudienceValue", // Add the audience claim here
                    Issuer = "YourIssuer",
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);
                return Ok(new { Token = tokenString });
            }
            return Unauthorized();
        }
    }
}
