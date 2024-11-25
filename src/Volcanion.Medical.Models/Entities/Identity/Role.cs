﻿using Volcanion.Core.Models.Entities;

namespace Volcanion.Medical.Models.Entities.Identity;

/// <summary>
/// Role
/// </summary>
public class Role : BaseEntity
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
