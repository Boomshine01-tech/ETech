using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using ETechEnergie.Server.Data;
using ETechEnergie.Server.Configuration;
using ETechEnergie.Server.Services;

var builder = WebApplication.CreateBuilder(args);

/// =======================================================
/// CONFIGURATION POSTGRESQL (RENDER / LOCAL)
/// =======================================================
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Render fournit DATABASE_URL
var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

if (!string.IsNullOrEmpty(databaseUrl))
{
    Console.WriteLine("🌍 DATABASE_URL détectée (Render)");

    var uri = new Uri(databaseUrl);
    var userInfo = uri.UserInfo.Split(':');
    var port = uri.Port > 0 ? uri.Port : 5432;

    Console.WriteLine($"📦 PostgreSQL Host     : {uri.Host}");
    Console.WriteLine($"📦 PostgreSQL Port     : {port}");
    Console.WriteLine($"📦 PostgreSQL Database : {uri.AbsolutePath.Trim('/')}");
    Console.WriteLine($"📦 PostgreSQL User     : {userInfo[0]}");

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

Console.WriteLine("🔌 Chaîne de connexion PostgreSQL prête");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

/// =======================================================
/// JWT AUTHENTICATION
/// =======================================================
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY") 
    ?? jwtSettings["SecretKey"] 
    ?? throw new InvalidOperationException("JWT SecretKey non configurée");

Console.WriteLine("🔐 Configuration JWT Authentication...");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
        ClockSkew = TimeSpan.Zero // Pas de tolérance sur l'expiration
    };

    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine($"❌ Authentification échouée: {context.Exception.Message}");
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            var username = context.Principal?.Identity?.Name;
            Console.WriteLine($"✅ Token validé pour: {username}");
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthorization();

Console.WriteLine("✅ JWT Authentication configurée");

/// =======================================================
/// SERVICES
/// =======================================================
builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("EmailSettings"));
builder.Services.Configure<BrevoSettings>(
    builder.Configuration.GetSection("BrevoSettings"));

builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IAuthService, AuthService>();

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
    
    // Ajouter le support JWT dans Swagger
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

// CORS - SÉCURISÉ (à ajuster selon vos besoins)
var allowedOrigins = Environment.GetEnvironmentVariable("ALLOWED_ORIGINS")?.Split(',') 
    ?? new[] { "https://etechenergie.onrender.com", "http://localhost:5000" };

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

/// =======================================================
/// BUILD APP
/// =======================================================
var app = builder.Build();

/// =======================================================
/// MIGRATIONS AUTOMATIQUES & SEED ADMIN
/// =======================================================
using (var scope = app.Services.CreateScope())
{
    try
    {
        Console.WriteLine("⏳ Application des migrations EF Core...");
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await context.Database.MigrateAsync();
        Console.WriteLine("✅ Migrations appliquées avec succès");
        
       await DbInitializer.Initialize(context);
        
        // Créer un utilisateur admin par défaut si aucun n'existe
        if (!await context.Users.AnyAsync())
        {
            Console.WriteLine("👤 Création de l'utilisateur admin par défaut...");
            var authService = scope.ServiceProvider.GetRequiredService<IAuthService>();
            
            var adminUser = new ETechEnergie.Shared.Models.User
            {
                Username = "admin",
                Email = "admin@etechenergie.com",
                PasswordHash = authService.HashPassword("Admin123!"), // À CHANGER EN PRODUCTION
                Role = "Admin",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };
            
            context.Users.Add(adminUser);
            await context.SaveChangesAsync();
            
            Console.WriteLine("✅ Utilisateur admin créé:");
            Console.WriteLine("   Username: admin");
            Console.WriteLine("   Password: Admin123!");
            Console.WriteLine("   ⚠️  CHANGEZ CE MOT DE PASSE EN PRODUCTION!");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("❌ ERREUR lors des migrations PostgreSQL");
        Console.WriteLine(ex.Message);
        throw;
    }
}

/// =======================================================
/// MIDDLEWARE
/// =======================================================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

// IMPORTANT: L'ordre est crucial
app.UseCors("SecureCors");
app.UseAuthentication(); // ⬅️ AVANT UseAuthorization
app.UseAuthorization();

app.MapControllers();
app.MapFallbackToFile("index.html");

/// =======================================================
/// PORT RENDER
/// =======================================================
var portEnv = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Urls.Add($"http://0.0.0.0:{portEnv}");

Console.WriteLine("==========================================");
Console.WriteLine("🚀 APPLICATION DÉMARRÉE");
Console.WriteLine($"🌐 Environnement : {app.Environment.EnvironmentName}");
Console.WriteLine($"🔗 Port          : {portEnv}");
Console.WriteLine($"🔐 JWT Auth      : Activée");
Console.WriteLine("==========================================");

app.Run();