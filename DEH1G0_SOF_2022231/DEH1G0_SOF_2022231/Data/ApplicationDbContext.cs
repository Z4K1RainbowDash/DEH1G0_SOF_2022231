using DEH1G0_SOF_2022231.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DEH1G0_SOF_2022231.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<AppUser> Users { get; set; }

        public DbSet<Torrent> Torrents { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AppUser>()
                .HasMany<Torrent>(s => s.Torrents)
                .WithMany(c => c.AppUsers)
                .UsingEntity(j => j.ToTable("TorrentUser"));
            

            base.OnModelCreating(builder);
        }
    }
}