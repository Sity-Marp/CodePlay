using Backend.Data;
using Backend.DTOs;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        //Regex för typiska grejer när man skapar konto.. tror inte det är något fel. 
        private static readonly Regex EmailRegex = new(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
        private static readonly Regex UsernameRegex = new(@"^[a-zA-Z0-9_]{3,20}$");
        private static readonly Regex PasswordRegex = new(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");

        public AuthController(AppDbContext context, IConfiguration config)
          { _context = context;
            _config = config; }
           

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            // Validering
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
                return BadRequest(new { message = "All fields are required" });

            var validation = ValidateInput(request.Username, request.Email, request.Password);
            if (validation != null) return BadRequest(new { message = validation });

            // Kolla om användaren/email redan existerar
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == request.Username || u.Email == request.Email);
            if (existingUser != null)
                return BadRequest(new { message = existingUser.Username == request.Username ? "Username already exists" : "Email already exists" });

            // Skapa ny användare
            var newUser = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = HashPassword(request.Password)
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User registered successfully" });
        }

        private string ValidateInput(string username, string email, string password)
        {
            return username.Length < 3 || username.Length > 20 ? "Användar namn måste vara mellan 3-20 characters" :
                   !UsernameRegex.IsMatch(username) ? "Användarnamn kan bara bestå av bokstäver, siffror, och understreck/underscore" :
                   !EmailRegex.IsMatch(email) ? "Inkorrekt email format" :
                   password.Length < 8 || password.Length > 24 ? "Lösenordet måste vara mellan 8-24 karaktärer" :
                   !PasswordRegex.IsMatch(password) ? "Lösenordet ska innehålla: stora och små bokstäver, minst en siffra samt ett specialtecken." :
                   null;
        }

        //simpel lösenord hash
        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            // Minsta krav: minst ett av Username/Email + Password
            if ((string.IsNullOrWhiteSpace(dto.Username) && string.IsNullOrWhiteSpace(dto.Email)) ||
                string.IsNullOrWhiteSpace(dto.Password))
            {
                return BadRequest(new { message = "Username/Email är inte giltiga" });
            }

            // Hitta användare via username ELLER email
            var user = await _context.Users.FirstOrDefaultAsync(u =>
                (!string.IsNullOrWhiteSpace(dto.Username) && u.Username == dto.Username) ||
                (!string.IsNullOrWhiteSpace(dto.Email) && u.Email == dto.Email));

            // Verifiera lösenord (samma hash som vid registrering)
            var hashed = HashPassword(dto.Password);
            if (user == null || user.PasswordHash != hashed)
            {
                return Unauthorized(new { message = "Ogiltigta värden" });
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim("id", user.Id.ToString()), // Lägg till "id" claim också
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return Ok(new
            {
                message = "Login ok",
                token,             // <— VIKTIG
                userId = user.Id,
                username = user.Username,
                email = user.Email
            });
        }

        // Hämta användarens framsteg för planet upplåsning
        [Authorize]
        [HttpGet("progress")]
        public async Task<IActionResult> GetUserProgress()
        {
            var userIdClaim = User.FindFirst("id") ?? User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
                return Unauthorized(new { message = "Ingen giltig user-id i token." });

            try
            {
                var userProgress = await _context.UserProgresses
                    .Where(up => up.UserId == userId)
                    .Select(up => new
                    {
                        up.Track,
                        up.CurrentLevel,
                        up.HighestUnlockedLevel,
                        up.TotalPoints,
                        up.TotalCorrect,
                        up.TotalIncorrect,
                    })
                    .ToListAsync();

                if (!userProgress.Any())
                {
                    return Ok(new[]
                    {
                        new
                        {
                            Track = "Html",
                            CurrentLevel = 1,
                            HighestUnlockedLevel = 1,
                            TotalPoints = 0,
                            TotalCorrect = 0,
                            TotalIncorrect = 0,
                        }
                    });
                }

                return Ok(userProgress);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Kunde inte hämta användarframsteg", error = ex.Message });
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            // Validering av input
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.NewPassword))
                return BadRequest(new { message = "Skriv in email/användarnamn och lösenord" });

            // Validera lösenord matchar confirmPassword
            if (request.NewPassword != request.ConfirmPassword)
                return BadRequest(new { message = "Lösenorden matchar inte" });

            // Validera nya lösenordet
            var passwordValidation = ValidateNewPassword(request.NewPassword);
            if (passwordValidation != null)
                return BadRequest(new { message = passwordValidation });

            // Kolla om input är email eller användarnamn
            bool isEmail = EmailRegex.IsMatch(request.Email);
            bool isUsername = UsernameRegex.IsMatch(request.Email);

            if (!isEmail && !isUsername)
                return BadRequest(new { message = "Ange ett korrekt email eller användarnamn" });

            // Hitta användaren via email eller username
            var user = await _context.Users.FirstOrDefaultAsync(u =>
                (isEmail && u.Email == request.Email) ||
                (isUsername && u.Username == request.Email));

            if (user == null)
                return BadRequest(new { message = "Ingen användare hittades med den angivna informationen" });

            // Uppdatera lösenord
            user.PasswordHash = HashPassword(request.NewPassword);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Lösenordet har uppdaterats" });
        }

        // Hjälpmetod för att validera lösenordets styrka
        private string ValidateNewPassword(string password)
        {
            return password.Length < 8 || password.Length > 24 ? "Lösenordet måste vara mellan 8-24 karaktärer" :
                !PasswordRegex.IsMatch(password) ? "Lösenordet ska innehålla: stora och små bokstäver, minst en siffra samt ett specialtecken." :
                null;
        }
    }
}