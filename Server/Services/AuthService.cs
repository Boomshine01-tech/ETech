using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using ETechEnergie.Server.Data;
using ETechEnergie.Shared.Models;

namespace ETechEnergie.Server.Services;

public interface IAuthService
{
    Task<LoginResponse> LoginAsync(LoginRequest request);
    string GenerateJwtToken(User user, bool rememberMe = false);
    string HashPassword(string password);
    bool VerifyPassword(string password, string passwordHash);
}

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly JwtConfiguration _jwtConfig;
    private readonly ILogger<AuthService> _logger;

    public AuthService(
        AppDbContext context,
        JwtConfiguration jwtConfig,
        ILogger<AuthService> logger)
    {
        _context = context;
        _jwtConfig = jwtConfig;
        _logger = logger;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        try
        {
            _logger.LogInformation("🔐 Tentative de connexion pour: {Username} | RememberMe: {RememberMe}", 
                request.Username, request.RememberMe);
            
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == request.Username && u.IsActive);

            if (user == null)
            {
                _logger.LogWarning("❌ Utilisateur introuvable: {Username}", request.Username);
                return new LoginResponse 
                { 
                    Success = false, 
                    Message = "Nom d'utilisateur ou mot de passe incorrect" 
                };
            }

            if (!VerifyPassword(request.Password, user.PasswordHash))
            {
                _logger.LogWarning("❌ Mot de passe incorrect pour: {Username}", request.Username);
                return new LoginResponse 
                { 
                    Success = false, 
                    Message = "Nom d'utilisateur ou mot de passe incorrect" 
                };
            }

            var token = GenerateJwtToken(user, request.RememberMe);
            
            var expirationHours = request.RememberMe ? 720.0 : _jwtConfig.ExpirationHours; 
            var expiresAt = DateTime.UtcNow.AddHours(expirationHours);

            _logger.LogInformation("✅ Connexion réussie pour: {Username} | RememberMe: {RememberMe} | Expiration: {ExpiresAt}", 
                user.Username, request.RememberMe, expiresAt);

            return new LoginResponse
            {
                Success = true,
                Message = "Connexion réussie",
                Token = token,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                RememberMe = request.RememberMe,
                ExpiresAt = expiresAt
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la connexion pour: {Username}", request.Username);
            return new LoginResponse 
            { 
                Success = false, 
                Message = "Une erreur est survenue lors de la connexion" 
            };
        }
    }

    public string GenerateJwtToken(User user, bool rememberMe = false)
    {
        var expirationHours = rememberMe ? 720.0 : _jwtConfig.ExpirationHours; 
        
        _logger.LogInformation(
            "🔑 Génération token JWT | User: {Username} | RememberMe: {RememberMe} | Durée: {Hours}h | Issuer: {Issuer} | Audience: {Audience}", 
            user.Username, rememberMe, expirationHours, _jwtConfig.Issuer, _jwtConfig.Audience);
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.SecretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim("username", user.Username),
            new Claim("role", user.Role),
            new Claim("rememberMe", rememberMe.ToString()) 
        };

        var expiration = DateTime.UtcNow.AddHours(expirationHours);

        var token = new JwtSecurityToken(
            issuer: _jwtConfig.Issuer,
            audience: _jwtConfig.Audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: expiration,
            signingCredentials: credentials
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        
        _logger.LogInformation(
            "✅ Token généré | User: {Username} | RememberMe: {RememberMe} | Expiration: {Expiration}", 
            user.Username, rememberMe, expiration);

        return tokenString;
    }

    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt(12));
    }

    public bool VerifyPassword(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
}
