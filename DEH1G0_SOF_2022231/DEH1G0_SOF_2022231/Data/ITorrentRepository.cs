using DEH1G0_SOF_2022231.Models;
using Microsoft.EntityFrameworkCore;

namespace DEH1G0_SOF_2022231.Data
{
    /// <summary>
    /// Repository interface for Torrent entities
    /// </summary>
    public interface ITorrentRepository : IGenericRepository<Torrent>
    {
        /// <summary>
        /// Asynchronously retrieves a collection of <see cref="AppUser"/>s associated with the <see cref="Torrent"/>
        /// with the given ID.
        /// </summary>
        /// <param name="id">The ID of the <see cref="Torrent"/> to retrieve <see cref="AppUser"/>s for.</param>
        /// <returns>A collection of <see cref="AppUser"/>s associated with the <see cref="Torrent"/>.</returns>

        Task<ICollection<AppUser>> GetUsersByTorrentId(int id);
    }
}
