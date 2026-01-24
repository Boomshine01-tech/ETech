using Microsoft.EntityFrameworkCore;
using ETechEnergie.Server.Data;
using ETechEnergie.Server.Configuration;
using ETechEnergie.Server.Services;
using System.Text.Json.Serialization;

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
/// SERVICES
/// =======================================================
builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("EmailSettings"));

builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});


/// =======================================================
/// BUILD APP
/// =======================================================
var app = builder.Build();


/// =======================================================
/// MIGRATIONS AUTOMATIQUES
/// =======================================================
using (var scope = app.Services.CreateScope())
{
    try
    {
        Console.WriteLine("⏳ Application des migrations EF Core...");
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await context.Database.MigrateAsync();
        Console.WriteLine("✅ Migrations appliquées avec succès");
        Dbinitializer.Initialize(context);
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
app.UseSwagger();
app.UseSwaggerUI();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseCors("AllowAll");
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
Console.WriteLine("==========================================");

app.Run();
