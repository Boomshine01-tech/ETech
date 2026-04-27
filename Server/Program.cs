using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using ETechEnergie.Server.Data;
using ETechEnergie.Server.Configuration;
using ETechEnergie.Server.Services;

var builder = WebApplication.CreateBuilder(args);

// CONFIGURATION POSTGRESQL (RENDER / LOCAL)

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

if (!string.IsNullOrEmpty(databaseUrl))
{
    Console.WriteLine(" DATABASE_URL détectée (Render)");
    var uri = new Uri(databaseUrl);
    var userInfo = uri.UserInfo.Split(':');
    var port = uri.Port > 0 ? uri.Port : 5432;

    Console.WriteLine($" PostgreSQL Host     : {uri.Host}");
    Console.WriteLine($" PostgreSQL Port     : {port}");
    Console.WriteLine($" PostgreSQL Database : {uri.AbsolutePath.Trim('/')}");
    Console.WriteLine($" PostgreSQL User     : {userInfo[0]}");

    connectionString =
        $"Host={uri.Host};" +
        $"Port={port};" +
        $"Database={uri.AbsolutePath.Trim('/')};" +
        $"Username={userInfo[0]};" +
        $"Password={userInfo[1]};" +
        $"SSL Mode=Require;" +
        $"Trust Server Certificate=true";
}
else
{
    Console.WriteLine("⚠️ DATABASE_URL absente → utilisation appsettings");
}

Console.WriteLine(" Chaîne de connexion PostgreSQL prête");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

Console.WriteLine(" Configuration JWT Authentication...");

var secretKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY");
var issuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
var audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");
var expirationHours = Environment.GetEnvironmentVariable("JWT_EXPIRATION_HOURS");

if (string.IsNullOrEmpty(secretKey))
{
    var jwtSettings = builder.Configuration.GetSection("JwtSettings");
    secretKey = jwtSettings["SecretKey"];
    issuer = jwtSettings["Issuer"];
    audience = jwtSettings["Audience"];
    expirationHours = jwtSettings["ExpirationHours"];
    Console.WriteLine(" JWT Config depuis appsettings.json");
}
else
{
    Console.WriteLine(" JWT Config depuis variables d'environnement Render");
}

if (string.IsNullOrEmpty(issuer))
{
    issuer = "ETechEnergie";
    Console.WriteLine($" JWT_ISSUER absent, utilisation par défaut: {issuer}");
}

if (string.IsNullOrEmpty(audience))
{
    audience = "ETechEnergieClient";
    Console.WriteLine($" JWT_AUDIENCE absent, utilisation par défaut: {audience}");
}

if (string.IsNullOrEmpty(expirationHours))
{
    expirationHours = "24";
}

if (string.IsNullOrEmpty(secretKey))
{
    throw new InvalidOperationException(
        "❌ JWT_SECRET_KEY non configurée !\n" +
        "Sur Render, ajoutez la variable d'environnement : JWT_SECRET_KEY\n" +
        "En local, ajoutez-la dans appsettings.json");
}

if (secretKey.Length < 32)
{
    throw new InvalidOperationException(
        $"❌ JWT_SECRET_KEY trop courte ({secretKey.Length} caractères)!\n" +
        "La clé doit faire au moins 32 caractères.");
}

Console.WriteLine("✅ Configuration JWT:");
Console.WriteLine($"   Issuer        : {issuer}");
Console.WriteLine($"   Audience      : {audience}");
Console.WriteLine($"   Expiration    : {expirationHours}h");
Console.WriteLine($"   SecretKey     : {secretKey.Substring(0, Math.Min(10, secretKey.Length))}... ({secretKey.Length} caractères)");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // Pour le développement local
    options.SaveToken = true;
    
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        
        ValidIssuer = issuer,
        ValidAudience = audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
        
        ClockSkew = TimeSpan.Zero,
        LogValidationExceptions = true
    };

    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine($"❌ Authentification échouée: {context.Exception.Message}");
            
            if (context.Exception is SecurityTokenExpiredException)
            {
                Console.WriteLine("   Raison: Token expiré");
            }
            else if (context.Exception is SecurityTokenInvalidAudienceException)
            {
                Console.WriteLine($"   Raison: Audience invalide");
                Console.WriteLine($"   Audience attendue: '{audience}'");
                
                try
                {
                    var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                    if (!string.IsNullOrEmpty(token))
                    {
                        var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
                        var jsonToken = handler.ReadToken(token) as System.IdentityModel.Tokens.Jwt.JwtSecurityToken;
                        var aud = jsonToken?.Audiences?.FirstOrDefault();
                        Console.WriteLine($"   Audience dans token: '{aud ?? "null"}'");
                    }
                }
                catch { }
            }
            else if (context.Exception is SecurityTokenInvalidIssuerException)
            {
                Console.WriteLine($"   Raison: Issuer invalide");
                Console.WriteLine($"   Issuer attendu: '{issuer}'");
            }
            
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            var username = context.Principal?.Identity?.Name;
            var role = context.Principal?.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
            Console.WriteLine($"✅ Token validé pour: {username} (Rôle: {role})");
            return Task.CompletedTask;
        },
        OnMessageReceived = context =>
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (!string.IsNullOrEmpty(token))
            {
                Console.WriteLine($" Token reçu (longueur: {token.Length})");
            }
            return Task.CompletedTask;
        },
        OnChallenge = context =>
        {
            Console.WriteLine($"⚠️ Challenge: {context.Error} - {context.ErrorDescription}");
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthorization();

Console.WriteLine("✅ JWT Authentication configurée avec succès");


builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("EmailSettings"));
builder.Services.Configure<BrevoSettings>(
    builder.Configuration.GetSection("BrevoSettings"));

builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddSingleton(new JwtConfiguration
{
    SecretKey = secretKey,
    Issuer = issuer,
    Audience = audience,
    ExpirationHours = double.Parse(expirationHours)
});

Console.WriteLine("✅ Services enregistrés");

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "ETech Energie API", Version = "v1" });
    
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
builder.Services.AddMemoryCache();

var allowedOrigins = Environment.GetEnvironmentVariable("ALLOWED_ORIGINS")?.Split(',') 
    ?? new[] { "https://etechenergie.onrender.com", "http://localhost:5000", "https://localhost:5001" };

    ?? new[] { "https://etechenergie.onrender.com", "http://localhost:5000", "https://localhost:5001", "https://localhost:58534" };

Console.WriteLine($"🌐 CORS Origins autorisées: {string.Join(", ", allowedOrigins)}");

builder.Services.AddCors(options =>
{
    options.AddPolicy("SecureCors", policy =>
    {
        policy.WithOrigins(allowedOrigins)
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
}); 

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    try
    {
        Console.WriteLine(" Application des migrations EF Core...");
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await context.Database.MigrateAsync();
        Console.WriteLine("✅ Migrations appliquées avec succès");
        
        await DbInitializer.Initialize(context);
        
    }
    catch (Exception ex)
    {
        Console.WriteLine("❌ ERREUR lors des migrations PostgreSQL");
        Console.WriteLine(ex.Message);
        throw;
    }
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseCors("SecureCors");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapFallbackToFile("index.html");


var portEnv = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Urls.Add($"http://0.0.0.0:{portEnv}");

Console.WriteLine("==========================================");
Console.WriteLine("🚀 APPLICATION DÉMARRÉE");
Console.WriteLine($"🌐 Environnement : {app.Environment.EnvironmentName}");
Console.WriteLine($"🔗 Port          : {portEnv}");
Console.WriteLine($"🔐 JWT Auth      : Activée");
Console.WriteLine($"   Issuer        : {issuer}");
Console.WriteLine($"   Audience      : {audience}");
Console.WriteLine("==========================================");

app.Run();


public class JwtConfiguration
{
    public string SecretKey { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public double ExpirationHours { get; set; }
}
