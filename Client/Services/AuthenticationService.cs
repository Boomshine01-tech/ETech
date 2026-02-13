using System.Net.Http.Json;
using System.Net.Http.Headers;
using Blazored.LocalStorage;
using ETechEnergie.Shared.Models;

namespace ETechEnergie.Client.Services;

public class AuthenticationService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;
    private const string TokenKey = "authToken";
    private const string UserKey = "currentUser";

    public event Action? OnAuthStateChanged;

    public AuthenticationService(HttpClient httpClient, ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", request);
            
            if (response.IsSuccessStatusCode)
            {
                var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
                
                if (loginResponse != null && loginResponse.Success && loginResponse.Token != null)
                {
                    // Sauvegarder le token dans LocalStorage
                    await _localStorage.SetItemAsync(TokenKey, loginResponse.Token);
                    
                    // Sauvegarder les infos utilisateur
                    await _localStorage.SetItemAsync(UserKey, new
                    {
                        loginResponse.Username,
                        loginResponse.Email,
                        loginResponse.Role
                    });

                    // Configurer le header Authorization pour les futures requêtes
                    _httpClient.DefaultRequestHeaders.Authorization = 
                        new AuthenticationHeaderValue("Bearer", loginResponse.Token);

                    // Notifier le changement d'état
                    OnAuthStateChanged?.Invoke();

                    return loginResponse;
                }
            }

            var errorResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
            return errorResponse ?? new LoginResponse 
            { 
                Success = false, 
                Message = "Erreur de connexion" 
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors de la connexion: {ex.Message}");
            return new LoginResponse 
            { 
                Success = false, 
                Message = "Une erreur est survenue lors de la connexion" 
            };
        }
    }

    public async Task<LoginResponse> RegisterAsync(RegisterRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/register", request);
            
            if (response.IsSuccessStatusCode)
            {
                var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
                
                if (loginResponse != null && loginResponse.Success && loginResponse.Token != null)
                {
                    await _localStorage.SetItemAsync(TokenKey, loginResponse.Token);
                    await _localStorage.SetItemAsync(UserKey, new
                    {
                        loginResponse.Username,
                        loginResponse.Email,
                        loginResponse.Role
                    });

                    _httpClient.DefaultRequestHeaders.Authorization = 
                        new AuthenticationHeaderValue("Bearer", loginResponse.Token);

                    OnAuthStateChanged?.Invoke();

                    return loginResponse;
                }
            }

            var errorResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
            return errorResponse ?? new LoginResponse 
            { 
                Success = false, 
                Message = "Erreur d'enregistrement" 
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors de l'enregistrement: {ex.Message}");
            return new LoginResponse 
            { 
                Success = false, 
                Message = "Une erreur est survenue lors de l'enregistrement" 
            };
        }
    }

    public async Task LogoutAsync()
    {
        await _localStorage.RemoveItemAsync(TokenKey);
        await _localStorage.RemoveItemAsync(UserKey);
        _httpClient.DefaultRequestHeaders.Authorization = null;
        OnAuthStateChanged?.Invoke();
    }

    public async Task<bool> IsAuthenticatedAsync()
    {
        var token = await _localStorage.GetItemAsync<string>(TokenKey);
        return !string.IsNullOrEmpty(token);
    }

    public async Task<bool> IsAdminAsync()
    {
        try
        {
            var user = await _localStorage.GetItemAsync<dynamic>(UserKey);
            return user?.Role == "Admin";
        }
        catch
        {
            return false;
        }
    }

    public async Task<(string? Username, string? Email, string? Role)> GetCurrentUserAsync()
    {
        try
        {
            var user = await _localStorage.GetItemAsync<dynamic>(UserKey);
            return (user?.Username?.ToString(), user?.Email?.ToString(), user?.Role?.ToString());
        }
        catch
        {
            return (null, null, null);
        }
    }

    public async Task InitializeAsync()
    {
        var token = await _localStorage.GetItemAsync<string>(TokenKey);
        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("Bearer", token);
        }
    }

    public async Task<string?> GetTokenAsync()
    {
        return await _localStorage.GetItemAsync<string>(TokenKey);
    }
}