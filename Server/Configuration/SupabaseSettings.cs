namespace ETechEnergie.Server.Configuration;

/// <summary>
/// Paramètres de connexion à Supabase Storage, utilisés pour l'upload des images
/// (produits, formations, réalisations). Renseignés via variables d'environnement
/// (SUPABASE_URL, SUPABASE_SERVICE_ROLE_KEY, SUPABASE_BUCKET) — voir Program.cs.
/// </summary>
public class SupabaseSettings
{
    /// <summary>URL du projet Supabase, ex: https://xxxxxxxx.supabase.co</summary>
    public string Url { get; set; } = string.Empty;

    /// <summary>
    /// Clé "service_role" (secrète, jamais exposée au client). Nécessaire pour uploader
    /// dans un bucket sans dépendre des policies RLS côté navigateur.
    /// </summary>
    public string ServiceRoleKey { get; set; } = string.Empty;

    /// <summary>Nom du bucket Supabase Storage où sont rangées toutes les images du site.</summary>
    public string Bucket { get; set; } = "images";
}
