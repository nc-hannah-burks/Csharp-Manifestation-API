using Microsoft.EntityFrameworkCore;
using ManifestationApi.Models;

namespace ManifestationApi.Models
{
    public class ManifestationContext : DbContext
    {
        public ManifestationContext(DbContextOptions<ManifestationContext> options)
            : base(options)
        {
        }


        public DbSet<ManifestationUser> ManifestationUsers { get; set; }
        public DbSet<ManifestationReminder> ManifestationReminders { get; set; }
        public DbSet<Manifestation> Manifestations { get; set; } = default!;

    }
}

