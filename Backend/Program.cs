// ===== Alla using måste ligga överst i filen =====
using Backend.Data;                    // vår AppDbContext
using Microsoft.EntityFrameworkCore;   // UseSqlServer
using Microsoft.OpenApi.Models;        // Swagger info
using Backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// -------------------------------------------------------------
// OpenAPI/Swagger (så du kan testa API:et i webben)
// -------------------------------------------------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CodePlay API", Version = "v1" });
});

// -------------------------------------------------------------
// EF Core: koppla in SQL Server/LocalDB via appsettings.json
// -------------------------------------------------------------
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// (AuthController)
builder.Services.AddControllers();
// JWT-konfiguration: berättar hur vi ska läsa/validera tokens
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,     
            ValidateAudience = false,   
            ValidateLifetime = true,  
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)) 
        };
    });
var app = builder.Build();

// -------------------------------------------------------------
// Middleware-pipeline
// -------------------------------------------------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// (JWT kommer senare)
// app.UseAuthentication();
// app.UseAuthorization();

