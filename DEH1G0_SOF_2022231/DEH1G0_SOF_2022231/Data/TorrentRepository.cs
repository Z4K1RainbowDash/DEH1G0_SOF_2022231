using DEH1G0_SOF_2022231.Models;

namespace DEH1G0_SOF_2022231.Data;

/// <summary>
/// Repository class for Torrent entities
/// </summary>
public class TorrentRepository : GenericRepository<Torrent>, ITorrentRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TorrentRepository"/> class.
    /// </summary>
    /// <param name="context">The <see cref="ApplicationDbContext"/> to use for database operations.</param>
    public TorrentRepository(ApplicationDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Asynchronously retrieves a collection of <see cref="AppUser"/>s associated with the <see cref="Torrent"/>
    /// with the given ID.
    /// </summary>
    /// <param name="id">The ID of the <see cref="Torrent"/> to retrieve <see cref="AppUser"/>s for.</param>
    /// <returns>A collection of <see cref="AppUser"/>s associated with the <see cref="Torrent"/>.</returns>
    public async Task<ICollection<AppUser>> GetUsersByTorrentId(string id)
    {
        var torrent = await this.GetByIdAsync(id);
        if (torrent == null)
        {
            throw new NullReferenceException("Torrent not found.");
        }
        return torrent.AppUsers;
    }
}

