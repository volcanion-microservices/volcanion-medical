using Volcanion.Core.Models.Entities;

namespace Volcanion.Medical.Models.Entities.Identity;

/// <summary>
/// GrantPermission
/// </summary>
public class GrantPermission : BaseEntity
{
    /// <summary>
    /// AccountId
    /// </summary>
    public Guid AccountId { get; set; }

    /// <summary>
    /// RolePermissionId
    /// </summary>
    public Guid RolePermissionId { get; set; }

    /// <summary>
    /// Account
    /// </summary>
    public Account Account { get; set; } = null!;

    /// <summary>
    /// RolePermissions
    /// </summary>
    public RolePermission RolePermission { get; set; } = null!;
}
