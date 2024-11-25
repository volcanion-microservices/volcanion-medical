using Volcanion.Core.Models.Entities;

namespace Volcanion.Medical.Models.Entities.Identity;

/// <summary>
/// Permission
/// </summary>
public class Permission : BaseEntity
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// RolePermissions
    /// </summary>
    public ICollection<RolePermission> RolePermissions { get; set; } = [];
}
