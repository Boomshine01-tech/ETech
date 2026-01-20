using Microsoft.EntityFrameworkCore;
using ETechEnergie.Server.Data;
using ETechEnergie.Server.Configuration;
using ETechEnergie.Server.Services;
using System.Text.Json.Serialization;
using Npgsql.EntityFrameworkCore.PostgreSQL;

var builder = WebApplication.CreateBuilder(args);

// ✅ IMPORTANT : Charger appsettings.Docker.json si en environnement Docker
if (builder.Environment.IsProduction())
{
    builder.Configuration.AddJsonFile("appsettings.Docker.json", optional: true, reloadOnChange: true);
}

// Configuration de la base de données
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

Console.WriteLine($"🔌 Connexion à la base de données : {connectionString?.Split(';')[0]}");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        connectionString,
        npgsqlOptionsAction: npgsqlOptions =>
        {
            npgsqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorCodesToAdd: null);
        }));

// Configuration des emails
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddScoped<IEmailService, EmailService>();

// Configuration des contrôleurs
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// ✅ NOUVEAU : Attendre que PostgreSQL soit prêt et initialiser la base
using (var scope = app.Services.CreateScope())
{
    var retryCount = 0;
    const int maxRetries = 10;
    
    while (retryCount < maxRetries)
    {
        try
        {
            Console.WriteLine($"⏳ Tentative de connexion à PostgreSQL ({retryCount + 1}/{maxRetries})...");
            
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            
            // ✅ Attendre que la base de données soit accessible
            await context.Database.CanConnectAsync();
            
            Console.WriteLine("✅ Connexion à PostgreSQL établie");
            
            // ✅ Appliquer les migrations ou créer la base
            await context.Database.EnsureCreatedAsync();
            
            // ✅ Initialiser les données
            await DbInitializer.Initialize(context);
            
            Console.WriteLine("✅ Base de données initialisée avec succès");
            break;
        }
        catch (Exception ex)
        {
            retryCount++;
            Console.WriteLine($"❌ Erreur de connexion ({retryCount}/{maxRetries}): {ex.Message}");
            
            if (retryCount >= maxRetries)
            {
                Console.WriteLine("❌ Impossible de se connecter à PostgreSQL après plusieurs tentatives");
                throw;
            }
            
            Console.WriteLine("⏳ Nouvelle tentative dans 5 secondes...");
            await Task.Delay(5000);
        }
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseWebAssemblyDebugging();
}

// ✅ En production, activer Swagger aussi (utile pour tester dans Docker)
if (app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseCors("AllowBlazorClient");
app.UseRouting();
app.UseAuthorization();

app.MapControllers();
app.MapFallbackToFile("index.html");

Console.WriteLine("🚀 Application démarrée");
Console.WriteLine($"🌐 Environnement : {app.Environment.EnvironmentName}");
Console.WriteLine($"🔗 URL : http://localhost:8080");

app.Run();