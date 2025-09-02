using Backend.Data;
using Backend.DTOs;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Backend.Services
{
    public class AuthService
    {
        private readonly AppDbContext _db; // v�r databas
        private readonly IConfiguration _config; // f�r att l�sa appsettings.json

        // Konstruktor
        public AuthService(AppDbContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }
        //Logga in och f� JWT-token.Jwt-token �r f�r att verifiera att anv�ndaren �r inloggad
        public async Task<string>LoginAsync(LoginDto dto)
        {
            //1) Hitta user med anv�ndarnamn eller email
            var user = await _db.Users
                               .FirstOrDefaultAsync(u => u.Username == dto.Username || u.Email == dto.Email);
            if(user == null)
            {
                throw new Exception("Anv�ndaren hittades inte");
            }
            //2) Verfiera l�senordet 
            var isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
            if (!isPasswordValid)
            {
                throw new Exception("Inkorrekt l�senord");

            }
            return GenerateJwt(user);   
        }
        private string GenerateJwt(User user)
        {
            // Claims = info som f�ljer med tokenen (vem �r inloggad)
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email)

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2), // giltig i 2h, anv�ndaren m�ste logga in igen efter det
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}