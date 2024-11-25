namespace Volcanion.Medical.Models.Response.BOs;

/// <summary>
/// RolePermissionResponseBO
/// </summary>
public class RolePermissionResponseBO
{
    /// <summary>
    /// GUID
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// IsActived
    /// </summary>
    public bool IsActived { get; set; }

    /// <summary>
    /// IsDeleted
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// RoleId
    /// </summary>
    public Guid RoleId { get; set; }

    /// <summary>
    /// PermissionId
    /// </summary>
    public Guid PermissionId { get; set; }

    /// <summary>
    /// GrantPermissionId
    /// </summary>
    public Guid GrantPermissionId { get; set; }

    /// <summary>
    /// CreatedAt
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// CreatedId
    /// </summary>
    public string? CreatedBy { get; set; }

    /// <summary>
    /// UpdatedAt
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// UpdatedId
    /// </summary>
    public string? UpdatedBy { get; set; }
}
