
using Backend.Data;                    // vår AppDbContext
using Microsoft.EntityFrameworkCore;   // UseSqlServer
using Microsoft.OpenApi.Models;        
using Backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
var builder = WebApplication.CreateBuilder(args);


// OpenAPI/Swagger 

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
builder.Services.AddScoped<PlayService>();


//controllers

builder.Services.AddControllers()
    .AddJsonOptions(o =>
    {
        // Gör att enums serialiseras som STRÄNGAR ("HTML") i stället för heltal (0)
        o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
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

//app.UseHttpsRedirection();
app.UseCors("AllowReactApp");

app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

app.Run();