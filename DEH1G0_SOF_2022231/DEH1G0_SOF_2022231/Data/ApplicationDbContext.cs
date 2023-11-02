using DEH1G0_SOF_2022231.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DEH1G0_SOF_2022231.Data;

/// <summary>
/// Represents the application's database context.
/// </summary>
public class ApplicationDbContext : IdentityDbContext
{
    /// <summary>
    /// Gets or sets the collection of AppUsers in the database.
    /// </summary>
    public new DbSet<AppUser> Users { get; set; }

    /// <summary>
    /// Gets or sets the collection of Torrents in the database.
    /// </summary>
    public DbSet<Torrent> Torrents { get; set; }

    /// <summary>
    /// Gets or sets the collection of TorrentLogs in the database.
    /// </summary>
    public DbSet<TorrentLog> TorrentLogs { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
    /// </summary>
    /// <param name="options"></param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<IdentityRole>().HasData(
            new { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
            new { Id = "2", Name = "Normal User", NormalizedName = "NORMAL USER" }
            );

        PasswordHasher<AppUser> ph = new PasswordHasher<AppUser>();
        AppUser admin = new AppUser
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = "Admin",
            LastName = "Admin",
            Email = "admin@admin.com",
            EmailConfirmed = true,
            UserName = "admin@admin.com",
            NormalizedUserName = "ADMIN@ADMIN.COM"
        };
        admin.PasswordHash = ph.HashPassword(admin, "admin@admin.com");
        builder.Entity<AppUser>().HasData(admin);

        builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
        {
            RoleId = "1",
            UserId = admin.Id
        });

        builder.Entity<TorrentLog>()
            .HasOne(t => t.Torrent)
            .WithMany()
            .HasForeignKey(t => t.TorrentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<TorrentLog>()
            .HasOne<AppUser>()
            .WithMany()
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);


        builder.Entity<AppUser>()
            .HasMany<Torrent>(s => s.Torrents)
            .WithMany(c => c.AppUsers)
            .UsingEntity(j => j.ToTable("TorrentUser"));

        base.OnModelCreating(builder);
    }
}
