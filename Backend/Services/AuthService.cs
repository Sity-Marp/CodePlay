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
        private readonly AppDbContext _db; // vår databas
        private readonly IConfiguration _config; // för att läsa appsettings.json

        // Konstruktor
        public AuthService(AppDbContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }
        //Logga in och få JWT-token.Jwt-token är för att verifiera att användaren är inloggad
        public async Task<string>LoginAsync(LoginDto dto)
        {
            //1) Hitta user med användarnamn eller email
            var user = await _db.Users
                               .FirstOrDefaultAsync(u => u.Username == dto.Username || u.Email == dto.Email);
            if(user == null)
            {
                throw new Exception("Användaren hittades inte");
            }
            //2) Verfiera lösenordet 
            var isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
            if (!isPasswordValid)
            {
                throw new Exception("Inkorrekt lösenord");

            }
            return GenerateJwt(user);   
        }
        private string GenerateJwt(User user)
        {
            // Claims = info som följer med tokenen (vem är inloggad)
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
                expires: DateTime.UtcNow.AddHours(2), // giltig i 2h, användaren måste logga in igen efter det
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}