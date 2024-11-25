using Volcanion.Medical.Models.Entities.Identity;

namespace Volcanion.Medical.Models.Cache;

/// <summary>
/// AccountCache
/// </summary>
public class AccountCache
{
    /// <summary>
    /// AccountData
    /// </summary>
    public Account AccountData { get; set; } = null!;

    /// <summary>
    /// RefreshToken
    /// </summary>
    public string RefreshToken { get; set; } = null!;
}
