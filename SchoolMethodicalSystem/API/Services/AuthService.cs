using API.Data.Models;
using API.Data;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Services
{
    public interface IAuthService
    {
        Task<(bool Success, string Token, string Message)> LoginAsync(LoginRequest request);
    }

    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly IConfiguration _cfg;

        public AuthService(AppDbContext db, IConfiguration cfg)
        {
            _db = db;
            _cfg = cfg;
        }

        public async Task<(bool Success, string Token, string Message)> LoginAsync(LoginRequest request)
        {
            var user = await _db.Users
                .FirstOrDefaultAsync(u => u.Login == request.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return (false, null, "Invalid credentials");

            var token = GenerateJwtToken(user);
            return (true, token, "OK");
        }

        private string GenerateJwtToken(User user)
        {
            var jwt = _cfg.GetSection("Jwt");

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Role, user.Role)   // Admin / Manager / Teacher
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwt["Issuer"],
                audience: jwt["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(jwt["ExpireMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
