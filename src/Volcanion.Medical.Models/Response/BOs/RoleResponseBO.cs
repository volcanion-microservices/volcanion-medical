namespace Volcanion.Medical.Models.Response.BOs;

/// <summary>
/// RoleResponseBO
/// </summary>
public class RoleResponseBO
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
    /// Name
    /// </summary>
    public string Name { get; set; }

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
