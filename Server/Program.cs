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
        // Pooling activé (comportement par défaut de Npgsql) : une connexion PostgreSQL/TLS est
        // coûteuse à établir (surtout vers Supabase, hébergé à distance). La désactiver forçait
        // une reconnexion complète à CHAQUE requête, ce qui ralentissait fortement toute l'appli.
        // ⚠️ Si vous utilisez le "Transaction pooler" de Supabase (port 6543 / pgbouncer), gardez
        // un œil sur les logs après ce changement : ce mode a des limitations connues avec le
        // pooling côté client. Dans ce cas, préférez le "Session pooler" (port 5432) plutôt que
        // de redésactiver Pooling entièrement.
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

// ───────────────────────────────────────────────────────────────
// SUPABASE STORAGE (upload des images produits / formations / réalisations)
// ───────────────────────────────────────────────────────────────
var supabaseUrl = Environment.GetEnvironmentVariable("SUPABASE_URL")
    ?? builder.Configuration["Supabase:Url"] ?? "";
var supabaseServiceRoleKey = Environment.GetEnvironmentVariable("SUPABASE_SERVICE_ROLE_KEY")
    ?? builder.Configuration["Supabase:ServiceRoleKey"] ?? "";
var supabaseBucket = Environment.GetEnvironmentVariable("SUPABASE_BUCKET")
    ?? builder.Configuration["Supabase:Bucket"] ?? "images";

if (string.IsNullOrEmpty(supabaseUrl) || string.IsNullOrEmpty(supabaseServiceRoleKey))
{
    Console.WriteLine("⚠️  SUPABASE_URL / SUPABASE_SERVICE_ROLE_KEY non définies : les uploads d'images échoueront tant qu'elles ne sont pas configurées.");
}
else
{
    Console.WriteLine($"✅ Supabase Storage configuré (bucket: \"{supabaseBucket}\")");
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

Console.WriteLine("✅ Services enregistrés");

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

// Compression des réponses (gzip/brotli) : réduit nettement la taille des réponses JSON
// (listes de produits/formations/réalisations) et donc le temps de chargement perçu,
// surtout sur connexion mobile.
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
    ?? new[] { "https://etechenergie.onrender.com", "http://localhost:5000", "https://localhost:5001", "https://localhost:58534", "http://127.0.0.1:63624" };

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

// Doit être l'un des tout premiers middlewares du pipeline pour compresser toutes les réponses.
app.UseResponseCompression();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseCors("SecureCors");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapFallbackToFile("index.html");


// var portEnv = Environment.GetEnvironmentVariable("PORT") ?? "8080";
// app.Urls.Add($"http://0.0.0.0:{portEnv}");

Console.WriteLine("==========================================");
Console.WriteLine("🚀 APPLICATION DÉMARRÉE");
Console.WriteLine($"🌐 Environnement : {app.Environment.EnvironmentName}");
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
