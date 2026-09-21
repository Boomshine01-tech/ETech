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
    private const string RememberMeKey = "rememberMe";
    private const string TokenExpirationKey = "tokenExpiration";

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
                    
                    await _localStorage.SetItemAsync(TokenKey, loginResponse.Token);
                    
                    var userData = new
                    {
                        loginResponse.Username,
                        loginResponse.Email,
                        loginResponse.Role
                    };
                    await _localStorage.SetItemAsync(UserKey, userData);

                    await _localStorage.SetItemAsync(RememberMeKey, loginResponse.RememberMe);

                    if (loginResponse.ExpiresAt.HasValue)
                    {
                        await _localStorage.SetItemAsync(TokenExpirationKey, loginResponse.ExpiresAt.Value);
                    }

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
                Message = "Erreur de connexion" 
            };
        }
        catch (Exception ex)
        {
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
                    await _localStorage.SetItemAsync(RememberMeKey, false); 
                    
                    if (loginResponse.ExpiresAt.HasValue)
                    {
                        await _localStorage.SetItemAsync(TokenExpirationKey, loginResponse.ExpiresAt.Value);
                    }

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
        await _localStorage.RemoveItemAsync(RememberMeKey);
        await _localStorage.RemoveItemAsync(TokenExpirationKey);
        
        _httpClient.DefaultRequestHeaders.Authorization = null;
        
        OnAuthStateChanged?.Invoke();
    }

    public async Task<bool> IsAuthenticatedAsync()
    {
        try
        {
            var token = await _localStorage.GetItemAsync<string>(TokenKey);
            
            if (string.IsNullOrEmpty(token))
            {
                return false;
            }

            var isExpired = await IsTokenExpiredAsync();
            
            if (isExpired)
            {
                
                await LogoutAsync();
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> IsTokenExpiredAsync()
    {
        try
        {
            var expiration = await _localStorage.GetItemAsync<DateTime?>(TokenExpirationKey);
            
            if (!expiration.HasValue)
            {
                return false; 
            }

            var isExpired = DateTime.UtcNow >= expiration.Value;
            
            if (isExpired)
            {
                Console.WriteLine($" Token expiré depuis: {(DateTime.UtcNow - expiration.Value).TotalHours:F1}h");
            }
            else
            {
                var timeLeft = expiration.Value - DateTime.UtcNow;
            }
            
            return isExpired;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> IsAdminAsync()
    {
        try
        {
            var user = await _localStorage.GetItemAsync<System.Text.Json.JsonElement>(UserKey);
            
            if (user.ValueKind == System.Text.Json.JsonValueKind.Undefined || 
                user.ValueKind == System.Text.Json.JsonValueKind.Null)
            {
                return false;
            }

            string? role = null;
            
            if (user.TryGetProperty("Role", out var roleProperty))
            {
                role = roleProperty.GetString();
            }
            else if (user.TryGetProperty("role", out var roleLowerProperty))
            {
                role = roleLowerProperty.GetString();
            }

            var isAdmin = role?.Equals("Admin", StringComparison.OrdinalIgnoreCase) ?? false;
            
            return isAdmin;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<(string? Username, string? Email, string? Role)> GetCurrentUserAsync()
    {
        try
        {
            var user = await _localStorage.GetItemAsync<System.Text.Json.JsonElement>(UserKey);
            
            if (user.ValueKind == System.Text.Json.JsonValueKind.Undefined || 
                user.ValueKind == System.Text.Json.JsonValueKind.Null)
            {
                return (null, null, null);
            }

            string? username = null, email = null, role = null;

            if (user.TryGetProperty("Username", out var usernameProperty))
                username = usernameProperty.GetString();
            
            if (user.TryGetProperty("Email", out var emailProperty))
                email = emailProperty.GetString();
            
            if (user.TryGetProperty("Role", out var roleProperty))
                role = roleProperty.GetString();
            
            
            return (username, email, role);
        }
        catch (Exception ex)
        {
            return (null, null, null);
        }
    }

    public async Task InitializeAsync()
    {
        try
        {
            
            var token = await _localStorage.GetItemAsync<string>(TokenKey);
            
            if (!string.IsNullOrEmpty(token))
            {
                
                var isExpired = await IsTokenExpiredAsync();
                
                if (isExpired)
                {
                    await LogoutAsync();
                }
                else
                {
                    _httpClient.DefaultRequestHeaders.Authorization = 
                        new AuthenticationHeaderValue("Bearer", token);                    
                }
            }
            else
            {
                Console.WriteLine("   Aucun token trouvé");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($" Erreur initialisation: {ex.Message}");
        }
    }

    public async Task<string?> GetTokenAsync()
    {
        try
        {
            return await _localStorage.GetItemAsync<string>(TokenKey);
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}
