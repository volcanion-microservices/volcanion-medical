namespace Volcanion.Medical.Models.Setting;

/// <summary>
/// AppSettings
/// </summary>
public class AppSettings
{
    /// <summary>
    /// Audience
    /// </summary>
    public string[] AllowedOrigins { get; set; } = ["*"];

    /// <summary>
    /// Audience
    /// </summary>
    public string[] GroupAccess { get; set; } = ["*"];

    /// <summary>
    /// Audience
    /// </summary>
    public string[] AllowedHeaders { get; set; } = ["*"];

    /// <summary>
    /// Audience
    /// </summary>
    public string[] AllowedMethods { get; set; } = ["*"];

    /// <summary>
    /// Issuer
    /// </summary>
    public string Audience { get; set; } = "volcanion-identity";
}
