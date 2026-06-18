using Microsoft.IdentityModel.Tokens;
using NotesAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NotesAPI.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration config;

        public JwtService(IConfiguration config)
        {
            this.config = config;
        }
        public string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:key"]));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: cred);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
