namespace Volcanion.Medical.Models.Request;

/// <summary>
/// AccountRegister
/// </summary>
public class AccountRegister
{
    /// <summary>
    /// Email
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    /// Password
    /// </summary>
    public string Password { get; set; } = null!;

    /// <summary>
    /// Fullname
    /// </summary>
    public string Fullname { get; set; } = null!;

    /// <summary>
    /// PhoneNumber
    /// </summary>
    public string PhoneNumber { get; set; } = null!;

    /// <summary>
    /// Address
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Birthday
    /// </summary>
    public DateTime? Birthday { get; set; }

    /// <summary>
    /// Issuer
    /// </summary>
    public string Issuer { get; set; } = null!;
}
