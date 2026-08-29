using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using ETechEnergie.Server.Configuration;

namespace ETechEnergie.Server.Services;

public interface ISupabaseStorageService
{
    /// <summary>
    /// Upload une image vers Supabase Storage, sous le "dossier" (préfixe de chemin) indiqué
    /// — ex: "products", "formations", "realisations" — afin de reproduire à l'identique
    /// l'organisation qui existait auparavant sous wwwroot/images/&lt;dossier&gt;/.
    /// Renvoie l'URL publique de l'image.
    /// </summary>
    Task<string> UploadImageAsync(IFormFile file, string folder);

    /// <summary>
    /// Supprime une image de Supabase Storage à partir de son URL publique.
    /// Best-effort : n'échoue jamais bruyamment (les erreurs sont journalisées et avalées),
    /// pour ne jamais faire échouer une suppression d'entité à cause d'un fichier déjà absent.
    /// </summary>
    Task DeleteImageAsync(string? imageUrl);
}

public class SupabaseStorageService : ISupabaseStorageService
{
    private readonly HttpClient _http;
    private readonly SupabaseSettings _settings;
    private readonly ILogger<SupabaseStorageService> _logger;

    public SupabaseStorageService(HttpClient http, IOptions<SupabaseSettings> settings, ILogger<SupabaseStorageService> logger)
    {
        _http = http;
        _settings = settings.Value;
        _logger = logger;
    }

    private bool IsConfigured =>
        !string.IsNullOrWhiteSpace(_settings.Url) && !string.IsNullOrWhiteSpace(_settings.ServiceRoleKey);

    public async Task<string> UploadImageAsync(IFormFile file, string folder)
    {
        if (!IsConfigured)
        {
            throw new InvalidOperationException(
                "Supabase Storage n'est pas configuré sur ce serveur. " +
                "Définissez les variables d'environnement SUPABASE_URL et SUPABASE_SERVICE_ROLE_KEY.");
        }

        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        var fileName = $"{Guid.NewGuid()}{extension}";
        // Reproduit l'organisation "wwwroot/images/<folder>/<fichier>" à l'intérieur du bucket.
        var objectPath = $"{folder}/{fileName}";

        var uploadUrl = $"{_settings.Url.TrimEnd('/')}/storage/v1/object/{_settings.Bucket}/{objectPath}";

        await using var fileStream = file.OpenReadStream();
        using var content = new StreamContent(fileStream);
        content.Headers.ContentType = new MediaTypeHeaderValue(
            string.IsNullOrWhiteSpace(file.ContentType) ? "application/octet-stream" : file.ContentType);

        using var request = new HttpRequestMessage(HttpMethod.Post, uploadUrl) { Content = content };
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _settings.ServiceRoleKey);
        request.Headers.Add("apikey", _settings.ServiceRoleKey);
        // Autorise l'écrasement si jamais le même chemin existe déjà (ne devrait pas arriver
        // avec un nom de fichier GUID, mais rend l'appel idempotent en cas de retry réseau).
        request.Headers.Add("x-upsert", "true");

        var response = await _http.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            _logger.LogError(
                "Échec de l'upload vers Supabase Storage ({Status}) pour '{Path}': {Error}",
                response.StatusCode, objectPath, error);
            throw new InvalidOperationException(
                $"Échec de l'upload vers Supabase Storage ({(int)response.StatusCode}). " +
                "Vérifiez que le bucket existe et que la clé service_role est correcte.");
        }

        var publicUrl = $"{_settings.Url.TrimEnd('/')}/storage/v1/object/public/{_settings.Bucket}/{objectPath}";
        _logger.LogInformation("Image uploadée sur Supabase Storage: {Path}", objectPath);

        return publicUrl;
    }

    public async Task DeleteImageAsync(string? imageUrl)
    {
        if (!IsConfigured || string.IsNullOrWhiteSpace(imageUrl))
        {
            return;
        }

        try
        {
            var marker = $"/storage/v1/object/public/{_settings.Bucket}/";
            var idx = imageUrl.IndexOf(marker, StringComparison.OrdinalIgnoreCase);
            if (idx < 0)
            {
                // Ce n'est pas une image hébergée sur Supabase Storage (ex: ancienne image
                // wwwroot legacy) : on ne tente pas de la supprimer.
                return;
            }

            var objectPath = imageUrl[(idx + marker.Length)..];
            var deleteUrl = $"{_settings.Url.TrimEnd('/')}/storage/v1/object/{_settings.Bucket}/{objectPath}";

            using var request = new HttpRequestMessage(HttpMethod.Delete, deleteUrl);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _settings.ServiceRoleKey);
            request.Headers.Add("apikey", _settings.ServiceRoleKey);

            var response = await _http.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning(
                    "Suppression Supabase Storage non confirmée pour '{Path}' ({Status})",
                    objectPath, response.StatusCode);
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Erreur lors de la suppression d'image sur Supabase Storage (ignorée)");
        }
    }
}
