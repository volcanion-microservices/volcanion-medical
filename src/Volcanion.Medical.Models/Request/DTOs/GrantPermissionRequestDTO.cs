namespace Volcanion.Medical.Models.Request.DTOs;

/// <summary>
/// GrantPermissionRequestDTO
/// </summary>
public class GrantPermissionRequestDTO
{
    /// <summary>
    /// AccountId
    /// </summary>
    public Guid AccountId { get; set; }

    /// <summary>
    /// RolePermissionId
    /// </summary>
    public Guid RolePermissionId { get; set; }
}
