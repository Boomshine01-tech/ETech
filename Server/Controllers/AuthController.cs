using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ETechEnergie.Server.Services;
using ETechEnergie.Shared.Models;

namespace ETechEnergie.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new LoginResponse 
            { 
                Success = false, 
                Message = "Données de connexion invalides" 
            });
        }

        var response = await _authService.LoginAsync(request);
        
        if (!response.Success)
        {
            return Unauthorized(response);
        }

        _logger.LogInformation("Utilisateur {Username} connecté avec succès", request.Username);
        return Ok(response);
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<ActionResult<LoginResponse>> Register([FromBody] RegisterRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new LoginResponse 
            { 
                Success = false, 
                Message = "Données d'enregistrement invalides" 
            });
        }

        var response = await _authService.RegisterAsync(request);
        
        if (!response.Success)
        {
            return BadRequest(response);
        }

        _logger.LogInformation("Nouvel utilisateur {Username} enregistré", request.Username);
        return Ok(response);
    }

    [HttpGet("validate")]
    [Authorize]
    public ActionResult<TokenValidationResponse> ValidateToken()
    {
        var username = User.Identity?.Name;
        var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

        return Ok(new TokenValidationResponse
        {
            IsValid = true,
            Username = username,
            Role = role
        });
    }

    [HttpGet("test-admin")]
    [Authorize(Roles = "Admin")]
    public ActionResult TestAdmin()
    {
        return Ok(new { Message = "Vous êtes bien authentifié en tant qu'Admin!", Username = User.Identity?.Name });
    }
}