using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using bowlingApp.Models;
using Microsoft.IdentityModel.Tokens;

namespace bowlingApp.Services
{
    public class TokenService(IConfiguration config) : ITokenService
    {
        public string? CreateToken(User user)
        {
            var tokenKey = config["TokenKey"] ?? "";
            if (tokenKey.Length < 64) return null;
            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
            
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.NameId, user.Username)
            };

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = creds,
                Issuer = config["Issuer"],
                Audience = config["Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
