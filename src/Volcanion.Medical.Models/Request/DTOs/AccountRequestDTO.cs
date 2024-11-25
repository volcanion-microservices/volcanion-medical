namespace Volcanion.Medical.Models.Request.DTOs;

/// <summary>
/// AccountRequestDTO
/// </summary>
public class AccountRequestDTO
{
    /// <summary>
    /// Id
    /// </summary>
    public Guid? Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Fullname
    /// </summary>
    public string Fullname { get; set; } = null!;

    /// <summary>
    /// Email
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    /// Password
    /// </summary>
    public string? Password { get; set; } = null!;

    /// <summary>
    /// Avatar
    /// </summary>
    public string? Avatar { get; set; }

    /// <summary>
    /// Address
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// PhoneNumber
    /// </summary>
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// IsActived
    /// </summary>
    public bool? IsActived { get; set; }
}
