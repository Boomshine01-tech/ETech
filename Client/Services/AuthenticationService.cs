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
            Console.WriteLine($"🔐 Tentative de connexion | User: {request.Username} | RememberMe: {request.RememberMe}");
            
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", request);
            
            Console.WriteLine($"   Status: {response.StatusCode}");
            
            if (response.IsSuccessStatusCode)
            {
                var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
                
                if (loginResponse != null && loginResponse.Success && loginResponse.Token != null)
                {
                    Console.WriteLine("✅ Connexion réussie");
                    Console.WriteLine($"   Username: {loginResponse.Username}");
                    Console.WriteLine($"   Role: {loginResponse.Role}");
                    Console.WriteLine($"   RememberMe: {loginResponse.RememberMe}");
                    Console.WriteLine($"   ExpiresAt: {loginResponse.ExpiresAt}");
                    Console.WriteLine($"   Token (début): {loginResponse.Token.Substring(0, Math.Min(30, loginResponse.Token.Length))}...");
                    
                    await _localStorage.SetItemAsync(TokenKey, loginResponse.Token);
                    Console.WriteLine($"   ✓ Token sauvegardé");
                    
                    var userData = new
                    {
                        loginResponse.Username,
                        loginResponse.Email,
                        loginResponse.Role
                    };
                    await _localStorage.SetItemAsync(UserKey, userData);
                    Console.WriteLine($"   ✓ User data sauvegardée");

                    await _localStorage.SetItemAsync(RememberMeKey, loginResponse.RememberMe);
                    Console.WriteLine($"   ✓ RememberMe sauvegardé: {loginResponse.RememberMe}");

                    if (loginResponse.ExpiresAt.HasValue)
                    {
                        await _localStorage.SetItemAsync(TokenExpirationKey, loginResponse.ExpiresAt.Value);
                        Console.WriteLine($"   ✓ Expiration sauvegardée: {loginResponse.ExpiresAt.Value}");
                    }

                    _httpClient.DefaultRequestHeaders.Authorization = 
                        new AuthenticationHeaderValue("Bearer", loginResponse.Token);
                    Console.WriteLine($"   ✓ Header Authorization configuré");

                    OnAuthStateChanged?.Invoke();

                    return loginResponse;
                }
            }

            var errorResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
            Console.WriteLine($"❌ Échec connexion: {errorResponse?.Message}");
            return errorResponse ?? new LoginResponse 
            { 
                Success = false, 
                Message = "Erreur de connexion" 
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Exception lors de la connexion: {ex.Message}");
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
            Console.WriteLine($"❌ Erreur lors de l'enregistrement: {ex.Message}");
            return new LoginResponse 
            { 
                Success = false, 
                Message = "Une erreur est survenue lors de l'enregistrement" 
            };
        }
    }

    public async Task LogoutAsync()
    {
        Console.WriteLine("🚪 Déconnexion...");
        
        await _localStorage.RemoveItemAsync(TokenKey);
        await _localStorage.RemoveItemAsync(UserKey);
        await _localStorage.RemoveItemAsync(RememberMeKey);
        await _localStorage.RemoveItemAsync(TokenExpirationKey);
        
        _httpClient.DefaultRequestHeaders.Authorization = null;
        
        Console.WriteLine("✅ Déconnexion réussie (toutes les données supprimées)");
        OnAuthStateChanged?.Invoke();
    }

    public async Task<bool> IsAuthenticatedAsync()
    {
        try
        {
            var token = await _localStorage.GetItemAsync<string>(TokenKey);
            
            if (string.IsNullOrEmpty(token))
            {
                Console.WriteLine($"🔍 IsAuthenticated: false (pas de token)");
                return false;
            }

            var isExpired = await IsTokenExpiredAsync();
            
            if (isExpired)
            {
                Console.WriteLine($"🔍 IsAuthenticated: false (token expiré)");
                
                await LogoutAsync();
                return false;
            }

            Console.WriteLine($"🔍 IsAuthenticated: true");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Erreur IsAuthenticated: {ex.Message}");
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
                Console.WriteLine("⚠️ Pas de date d'expiration stockée");
                return false; 
            }

            var isExpired = DateTime.UtcNow >= expiration.Value;
            
            if (isExpired)
            {
                Console.WriteLine($"⏰ Token expiré depuis: {(DateTime.UtcNow - expiration.Value).TotalHours:F1}h");
            }
            else
            {
                var timeLeft = expiration.Value - DateTime.UtcNow;
                Console.WriteLine($"⏰ Token expire dans: {timeLeft.TotalDays:F1} jours ({timeLeft.TotalHours:F1}h)");
            }
            
            return isExpired;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Erreur IsTokenExpired: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> IsRememberedAsync()
    {
        try
        {
            return await _localStorage.GetItemAsync<bool>(RememberMeKey);
        }
        catch
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
                Console.WriteLine("⚠️ IsAdmin: Pas de données utilisateur");
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
            Console.WriteLine($"🔍 IsAdmin: {isAdmin} (Role: {role ?? "null"})");
            
            return isAdmin;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Erreur IsAdmin: {ex.Message}");
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
                Console.WriteLine("⚠️ GetCurrentUser: Pas de données utilisateur");
                return (null, null, null);
            }

            string? username = null, email = null, role = null;

            if (user.TryGetProperty("Username", out var usernameProperty))
                username = usernameProperty.GetString();
            
            if (user.TryGetProperty("Email", out var emailProperty))
                email = emailProperty.GetString();
            
            if (user.TryGetProperty("Role", out var roleProperty))
                role = roleProperty.GetString();
            
            Console.WriteLine($"👤 GetCurrentUser: {username} | {role}");
            
            return (username, email, role);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Erreur GetCurrentUser: {ex.Message}");
            return (null, null, null);
        }
    }

    public async Task InitializeAsync()
    {
        try
        {
            Console.WriteLine("🔧 Initialisation AuthenticationService...");
            
            var token = await _localStorage.GetItemAsync<string>(TokenKey);
            
            if (!string.IsNullOrEmpty(token))
            {
                Console.WriteLine($"   Token trouvé (longueur: {token.Length})");
                
                var isExpired = await IsTokenExpiredAsync();
                
                if (isExpired)
                {
                    Console.WriteLine("   ⚠️ Token expiré - Suppression");
                    await LogoutAsync();
                }
                else
                {
                    _httpClient.DefaultRequestHeaders.Authorization = 
                        new AuthenticationHeaderValue("Bearer", token);
                    Console.WriteLine("   ✓ Header Authorization configuré");
                    
                    var isRemembered = await IsRememberedAsync();
                    Console.WriteLine($"   RememberMe: {isRemembered}");
                }
            }
            else
            {
                Console.WriteLine("   Aucun token trouvé");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Erreur initialisation: {ex.Message}");
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
            Console.WriteLine($"❌ Erreur GetToken: {ex.Message}");
            return null;
        }
    }

    public async Task<DateTime?> GetTokenExpirationAsync()
    {
        try
        {
            return await _localStorage.GetItemAsync<DateTime?>(TokenExpirationKey);
        }
        catch
        {
            return null;
        }
    }
}
