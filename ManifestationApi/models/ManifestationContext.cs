using Microsoft.EntityFrameworkCore;

namespace ManifestationApi.Models;

public class ManifestationContext : DbContext
{
    public ManifestationContext(DbContextOptions<ManifestationContext> options)
        : base(options)
    {
    }

    public DbSet<ManifestationUser> ManifestationUsers { get; set; } = null!;
}
