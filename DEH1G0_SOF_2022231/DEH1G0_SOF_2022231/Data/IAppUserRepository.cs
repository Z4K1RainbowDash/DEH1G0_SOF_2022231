using DEH1G0_SOF_2022231.Helpers;
using DEH1G0_SOF_2022231.Models;

namespace DEH1G0_SOF_2022231.Data;

/// <summary>
/// Repository interface for AppUser entities
/// </summary>
public interface IAppUserRepository : IGenericRepository<AppUser>
{
    /// <summary>
    /// Asynchronously retrieves a collection of <see cref="Torrent"/>s associated with the <see cref="AppUser"/>
    /// with the given ID.
    /// </summary>
    /// <param name="id">The ID of the <see cref="AppUser"/> to retrieve <see cref="Torrent"/>s for.</param>
    /// <returns>A collection of <see cref="Torrent"/>s associated with the <see cref="AppUser"/>.</returns>
    Task<ICollection<Torrent>> GetTorrentsByUserId(string id);

    Task<PaginatedList<AppUser>> GetPaginatedAppUsersAsync(PageQueryParameters pageQueryParameters);

}