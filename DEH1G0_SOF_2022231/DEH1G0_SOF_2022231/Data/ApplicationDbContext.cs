using DEH1G0_SOF_2022231.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DEH1G0_SOF_2022231.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<AppUser> Users { get; set; }

        public DbSet<Torrent> Torrents { get; set; }

        public DbSet<TorrentLog> TorrentLogs { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TorrentLog>()
                .HasOne(t=> t.Torrent)
                .WithMany()
                .HasForeignKey(t=> t.TorrentId)
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
}