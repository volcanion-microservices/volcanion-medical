namespace Volcanion.Medical.Models.Response.BOs;

/// <summary>
/// GrantPermissionResponseBO
/// </summary>
public class GrantPermissionResponseBO
{
    /// <summary>
    /// GUID
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// AccountId
    /// </summary>
    public Guid AccountId { get; set; }

    /// <summary>
    /// RolePermissionId
    /// </summary>
    public Guid RolePermissionId { get; set; }

    /// <summary>
    /// RoleId
    /// </summary>
    public Guid RoleId { get; set; }

    /// <summary>
    /// RoleName
    /// </summary>
    public string RoleName { get; set; } = null!;

    /// <summary>
    /// PermissionId
    /// </summary>
    public Guid PermissionId { get; set; }

    /// <summary>
    /// PermissionName
    /// </summary>
    public string PermissionName { get; set; } = null!;
}
