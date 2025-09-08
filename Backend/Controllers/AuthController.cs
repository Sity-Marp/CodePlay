using Backend.Data;
using Backend.DTOs;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        //Regex för typiska grejer när man skapar konto.. tror inte det är något fel. 
        private static readonly Regex EmailRegex = new(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
        private static readonly Regex UsernameRegex = new(@"^[a-zA-Z0-9_]{3,20}$");
        private static readonly Regex PasswordRegex = new(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");

        public AuthController(AppDbContext context) => _context = context;

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

            return Ok(new { message = "User registered successfully", userId = newUser.Id });
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
     //Login

[AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            // Minsta krav: minst ett av Username/Email + Password
            if ((string.IsNullOrWhiteSpace(dto.Username) && string.IsNullOrWhiteSpace(dto.Email)) ||
                string.IsNullOrWhiteSpace(dto.Password))
            {
                return BadRequest(new { message = "Username or Email and Password are required" });
            }

            // Hitta användare via username ELLER email
            var user = await _context.Users.FirstOrDefaultAsync(u =>
                (!string.IsNullOrWhiteSpace(dto.Username) && u.Username == dto.Username) ||
                (!string.IsNullOrWhiteSpace(dto.Email) && u.Email == dto.Email));

            // Verifiera lösenord (samma hash som vid registrering)
            var hashed = HashPassword(dto.Password);
            if (user == null || user.PasswordHash != hashed)
            {
               
                return Unauthorized(new { message = "Invalid credentials" });
            }


            return Ok(new
            {
                message = "Login ok",
                userId = user.Id,
                username = user.Username,
                email = user.Email
               
            });
        }
    } }
