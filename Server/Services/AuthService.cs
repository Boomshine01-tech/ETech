using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ETechEnergie.Shared.Models;
using ETechEnergie.Server.Data;
using Microsoft.EntityFrameworkCore;


namespace ETechEnergie.Server.Services;

public interface IAuthService
{
    Task<LoginResponse> LoginAsync(LoginRequest request);
    Task<LoginResponse> RegisterAsync(RegisterRequest request);
    string GenerateJwtToken(User user);
    string HashPassword(string password);
    bool VerifyPassword(string password, string passwordHash);
    TokenValidationResponse ValidateToken(string token);
}

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthService> _logger;

    public AuthService(
        AppDbContext context, 
        IConfiguration configuration,
        ILogger<AuthService> logger)
    {
        _context = context;
        _configuration = configuration;
        _logger = logger;
    }


    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        try
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == request.Username && u.IsActive);

            if (user == null)
            {
                return new LoginResponse 
                { 
                    Success = false, 
                    Message = "Nom d'utilisateur ou mot de passe incorrect" 
                };
            }

            if (!VerifyPassword(request.Password, user.PasswordHash))
            {
                return new LoginResponse 
                { 
                    Success = false, 
                    Message = "Nom d'utilisateur ou mot de passe incorrect" 
                };
            }

            var token = GenerateJwtToken(user);

            return new LoginResponse
            {
                Success = true,
                Message = "Connexion réussie",
                Token = token,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la connexion");
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
            // Vérifier si l'utilisateur existe déjà
            if (await _context.Users.AnyAsync(u => u.Username == request.Username))
            {
                return new LoginResponse 
                { 
                    Success = false, 
                    Message = "Ce nom d'utilisateur est déjà utilisé" 
                };
            }

            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
            {
                return new LoginResponse 
                { 
                    Success = false, 
                    Message = "Cet email est déjà utilisé" 
                };
            }

            // Créer le nouvel utilisateur
            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = HashPassword(request.Password),
                Role = "User", // Par défaut, rôle User (Admin doit être défini manuellement)
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var token = GenerateJwtToken(user);

            return new LoginResponse
            {
                Success = true,
                Message = "Compte créé avec succès",
                Token = token,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de l'enregistrement");
            return new LoginResponse 
            { 
                Success = false, 
                Message = "Une erreur est survenue lors de la création du compte" 
            };
        }
    }

    public string GenerateJwtToken(User user)
    {
        
        private readonly JwtConfiguration _jwtConfig;
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _jwtConfig.Issuer,  
            audience: _jwtConfig.Audience
            claims: claims,
            expires: DateTime.UtcNow.AddHours(Convert.ToDouble(jwtSettings["ExpirationHours"] ?? "24")),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt(12));
    }

    public bool VerifyPassword(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }

    public TokenValidationResponse ValidateToken(string token)
    {
        try
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"] ?? throw new InvalidOperationException("JWT SecretKey non configurée");
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(secretKey);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ClockSkew = TimeSpan.Zero
            };

            var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
            
            return new TokenValidationResponse
            {
                IsValid = true,
                Username = principal.FindFirst(ClaimTypes.Name)?.Value,
                Role = principal.FindFirst(ClaimTypes.Role)?.Value
            };
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Token invalide");
            return new TokenValidationResponse { IsValid = false };
        }
    }
}
