using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using ETechEnergie.Server.Data;
using ETechEnergie.Server.Configuration;
using ETechEnergie.Server.Services;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);

// CONFIGURATION POSTGRESQL (RENDER / LOCAL)

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

var host     = Environment.GetEnvironmentVariable("DB_HOST");
var port     = Environment.GetEnvironmentVariable("DB_PORT")     ?? "5432";
var database = Environment.GetEnvironmentVariable("DB_NAME")     ?? "postgres";
var username = Environment.GetEnvironmentVariable("DB_USER")     ?? "postgres";
var password = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "";

if (!string.IsNullOrEmpty(host))
{
    Console.WriteLine($" DB_HOST détecté → {host}:{port}/{database}");
    connectionString =
        $"Host={host};" +
        $"Port={port};" +
        $"Database={database};" +
        $"Username={username};" +
        $"Password={password};" +
        $"SSL Mode=Require;" +
        $"Trust Server Certificate=true;" +
        $"Maximum Pool Size=20;" +
        $"Minimum Pool Size=1;";
}
else
{
    connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
              ?? "Host=localhost;Database=smartnest;Username=postgres;Password=postgres";
}

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
    Console.WriteLine(" JWT Config depuis variables d'environnement");
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
    throw new InvalidOperationException("JWT_SECRET_KEY non configurée !\n");
}

Console.WriteLine("Configuration JWT reussie:");
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
            Console.WriteLine($"Authentification échouée: {context.Exception.Message}");
            
            if (context.Exception is SecurityTokenExpiredException)
            {
                Console.WriteLine("   Raison: Token expiré");
            }
            else if (context.Exception is SecurityTokenInvalidAudienceException)
            {
                Console.WriteLine($"   Raison: Audience invalide");
                
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
            }
            
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            var username = context.Principal?.Identity?.Name;
            var role = context.Principal?.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
            return Task.CompletedTask;
        },
        OnMessageReceived = context =>
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            return Task.CompletedTask;
        },
        OnChallenge = context =>
        {
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthorization();

builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("EmailSettings"));
builder.Services.Configure<BrevoSettings>(
    builder.Configuration.GetSection("BrevoSettings"));

builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IAuthService, AuthService>();

var supabaseUrl = Environment.GetEnvironmentVariable("SUPABASE_URL")
    ?? builder.Configuration["Supabase:Url"] ?? "";
var supabaseServiceRoleKey = Environment.GetEnvironmentVariable("SUPABASE_SERVICE_ROLE_KEY")
    ?? builder.Configuration["Supabase:ServiceRoleKey"] ?? "";
var supabaseBucket = Environment.GetEnvironmentVariable("SUPABASE_BUCKET")
    ?? builder.Configuration["Supabase:Bucket"] ?? "images";

if (string.IsNullOrEmpty(supabaseUrl) || string.IsNullOrEmpty(supabaseServiceRoleKey))
{
    Console.WriteLine(" Erreur Config SupaBase.");
}

builder.Services.Configure<SupabaseSettings>(options =>
{
    options.Url = supabaseUrl;
    options.ServiceRoleKey = supabaseServiceRoleKey;
    options.Bucket = supabaseBucket;
});

builder.Services.AddHttpClient<ISupabaseStorageService, SupabaseStorageService>();

builder.Services.AddSingleton(new JwtConfiguration
{
    SecretKey = secretKey,
    Issuer = issuer,
    Audience = audience,
    ExpirationHours = double.Parse(expirationHours)
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
    options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/json" });
});
builder.Services.Configure<BrotliCompressionProviderOptions>(o => o.Level = System.IO.Compression.CompressionLevel.Fastest);
builder.Services.Configure<GzipCompressionProviderOptions>(o => o.Level = System.IO.Compression.CompressionLevel.Fastest);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMemoryCache();

var allowedOrigins = Environment.GetEnvironmentVariable("ALLOWED_ORIGINS")?.Split(',') 
    ?? new[] { "https://etechenergie.onrender.com", "http://localhost:5000", "https://localhost:5001", "https://localhost:58534", "http://127.0.0.1:63624" };

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

app.UseResponseCompression();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();

app.UseCors("SecureCors");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();


public class JwtConfiguration
{
    public string SecretKey { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public double ExpirationHours { get; set; }
}
