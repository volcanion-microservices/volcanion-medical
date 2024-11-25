using Volcanion.Core.Models.Entities;

namespace Volcanion.Medical.Models.Entities.Identity;

/// <summary>
/// RolePermission
/// </summary>
public class RolePermission : BaseEntity
{
    /// <summary>
    /// RoleId
    /// </summary>
    public Guid RoleId { get; set; }

    /// <summary>
    /// PermissionId
    /// </summary>
    public Guid PermissionId { get; set; }

    /// <summary>
    /// Role
    /// </summary>
    public Role Role { get; set; } = null!;

    /// <summary>
    /// Permission
    /// </summary>
    public Permission Permission { get; set; } = null!;

    /// <summary>
    /// GrantPermission
    /// </summary>
    public ICollection<GrantPermission> GrantPermissions { get; set; } = [];
}
