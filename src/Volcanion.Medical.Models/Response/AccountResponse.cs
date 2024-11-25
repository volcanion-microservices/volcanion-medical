using Volcanion.Medical.Models.Entities.Identity;

namespace Volcanion.Medical.Models.Response;

/// <summary>
/// AccountResponse
/// </summary>
public class AccountResponse
{
    /// <summary>
    /// Account
    /// </summary>
    public Account Account { get; set; }

    /// <summary>
    /// AccessToken
    /// </summary>
    public string AccessToken { get; set; } = null!;

    /// <summary>
    /// RefreshToken
    /// </summary>
    public string RefreshToken { get; set; } = null!;
}
