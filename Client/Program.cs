using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;
using ETechEnergie.Client;
using ETechEnergie.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// HttpClient configuré
builder.Services.AddScoped(sp => new HttpClient 
{ 
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) 
});

// Services existants
builder.Services.AddScoped<CartService>();
builder.Services.AddScoped<NotificationService>();

// ⬇️ NOUVEAUX SERVICES POUR L'AUTHENTIFICATION
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<AuthenticationService>();

// Initialiser l'authentification au démarrage
var host = builder.Build();

// Initialiser le token JWT dans HttpClient
var authService = host.Services.GetRequiredService<AuthenticationService>();
await authService.InitializeAsync();

await host.RunAsync();