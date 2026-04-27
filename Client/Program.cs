using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;
using ETechEnergie.Client;
using ETechEnergie.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient 
{ 
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) 
});

/*builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(
        builder.HostEnvironment.IsDevelopment()
            ? "http://localhost:8080/"
            : builder.HostEnvironment.BaseAddress
    )
});*/

// Services 
builder.Services.AddScoped<CartService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<AuthenticationService>();

// Initialiser authentification démarrage
var host = builder.Build();

// Initialiser token JWT dans HttpClient
var authService = host.Services.GetRequiredService<AuthenticationService>();
await authService.InitializeAsync();

await host.RunAsync();
