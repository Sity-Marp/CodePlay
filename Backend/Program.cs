// ===== Alla using m�ste ligga �verst i filen =====
using Backend.Data;                    // v�r AppDbContext
using Microsoft.EntityFrameworkCore;   // UseSqlServer
using Microsoft.OpenApi.Models;        // Swagger info
using Backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// -------------------------------------------------------------
// OpenAPI/Swagger (s� du kan testa API:et i webben)
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

//controllers
builder.Services.AddControllers();

//cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
        policy.WithOrigins("http://localhost:3000") // React-dev server
              .AllowAnyHeader()
              .AllowAnyMethod());
});

// JWT-konfiguration: ber�ttar hur vi ska l�sa/validera tokens
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
app.UseCors("AllowReactApp");
app.MapControllers();

// (JWT kommer senare)
// app.UseAuthentication();
// app.UseAuthorization();

app.Run();