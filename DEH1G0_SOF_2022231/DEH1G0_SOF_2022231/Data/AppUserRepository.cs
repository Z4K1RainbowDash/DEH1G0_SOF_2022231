using DEH1G0_SOF_2022231.Models;

namespace DEH1G0_SOF_2022231.Data;

    /// <summary>
    /// Repository class for AppUser entities
    /// </summary>
    public class AppUserRepository : GenericRepository<AppUser>, IAppUserRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppUserRepository"/> class.
        /// </summary>
        /// <param name="context">The <see cref="ApplicationDbContext"/> to use for database operations.</param>
        public AppUserRepository(ApplicationDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Asynchronously retrieves a collection of <see cref="Torrent"/>s associated with the <see cref="AppUser"/>
        /// with the given ID.
        /// </summary>
        /// <param name="id">The ID of the <see cref="AppUser"/> to retrieve <see cref="Torrent"/>s for.</param>
        /// <returns>A collection of <see cref="Torrent"/>s associated with the <see cref="AppUser"/>.</returns>
        public async Task<ICollection<Torrent>> GetTorrentsByUserId(string id)
        {
            var user = await this.GetByIdAsync(id);
            return user.Torrents;
        }
    }


