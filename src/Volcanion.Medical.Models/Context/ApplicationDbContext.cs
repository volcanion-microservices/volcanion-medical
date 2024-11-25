using Microsoft.EntityFrameworkCore;
using Volcanion.Medical.Models.Entities.Identity;

namespace Volcanion.Medical.Models.Context;

/// <summary>
/// ApplicationDbContext
/// </summary>
/// <param name="options"></param>
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Account
    /// </summary>
    public DbSet<Account> Account { get; set; }

    /// <summary>
    /// Roles
    /// </summary>
    public DbSet<Role> Roles { get; set; }

    /// <summary>
    /// Permissions
    /// </summary>
    public DbSet<Permission> Permissions { get; set; }

    /// <summary>
    /// RolePermissions
    /// </summary>
    public DbSet<RolePermission> RolePermissions { get; set; }

    /// <summary>
    /// GrantPermissions
    /// </summary>
    public DbSet<GrantPermission> GrantPermissions { get; set; }

    /// <summary>
    /// OnModelCreating
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Call base OnModelCreating method
        base.OnModelCreating(modelBuilder);

        // RolePermission: Relationship with Role
        modelBuilder.Entity<RolePermission>()
            .HasOne(rp => rp.Role)
            .WithMany(r => r.RolePermissions)
            .HasForeignKey(rp => rp.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

        // RolePermission: Relationship with Permission
        modelBuilder.Entity<RolePermission>()
            .HasOne(rp => rp.Permission)
            .WithMany(p => p.RolePermissions)
            .HasForeignKey(rp => rp.PermissionId)
            .OnDelete(DeleteBehavior.Restrict);

        // GrantPermission: Relationship with RolePermission
        modelBuilder.Entity<GrantPermission>()
            .HasOne(gp => gp.RolePermission)
            .WithMany(rp => rp.GrantPermissions)
            .HasForeignKey(gp => gp.RolePermissionId)
            .OnDelete(DeleteBehavior.Restrict);

        // GrantPermission: Relationship with Account
        modelBuilder.Entity<GrantPermission>()
            .HasOne(gp => gp.Account)
            .WithMany(a => a.GrantPermissions)
            .HasForeignKey(gp => gp.AccountId)
            .OnDelete(DeleteBehavior.Restrict);

        // Account: Unique constraint on Email
        modelBuilder.Entity<Account>()
            .HasIndex(a => a.Email)
            .IsUnique();

        modelBuilder.Entity<Role>().HasData(
            new Role { Name = "Admin" },
            new Role { Name = "Account" },
            new Role { Name = "Role" },
            new Role { Name = "Permission" },
            new Role { Name = "RolePermission" },
            new Role { Name = "GrantPermission" }
        );

        modelBuilder.Entity<Permission>().HasData(
            new Permission { Name = "All" },
            new Permission { Name = "Create" },
            new Permission { Name = "Read" },
            new Permission { Name = "Update" },
            new Permission { Name = "SoftDelete" },
            new Permission { Name = "HardDelete" },
            new Permission { Name = "Grant" }
        );
    }
}
