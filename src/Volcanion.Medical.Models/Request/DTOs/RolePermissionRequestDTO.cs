namespace Volcanion.Medical.Models.Request.DTOs;

/// <summary>
/// RolePermissionRequestDTO
/// </summary>
public class RolePermissionRequestDTO
{
    /// <summary>
    /// RoleId
    /// </summary>
    public Guid RoleId { get; set; }

    /// <summary>
    /// PermissionId
    /// </summary>
    public Guid PermissionId { get; set; }
}
