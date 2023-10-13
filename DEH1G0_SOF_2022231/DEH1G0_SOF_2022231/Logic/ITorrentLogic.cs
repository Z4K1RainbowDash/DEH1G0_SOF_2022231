using DEH1G0_SOF_2022231.Helpers;
using DEH1G0_SOF_2022231.Models;
using DEH1G0_SOF_2022231.Models.DTOs;

namespace DEH1G0_SOF_2022231.Logic;

/// <summary>
/// Contains the logic interface for building Ncore Url
/// </summary>
public interface ITorrentLogic
{
    /// <summary>
    /// Returns the NcoreUrl based on the input search text and categories.
    /// </summary>
    /// <param name="dto">DTO that contains the selected torrent categories and the search text.</param>
    /// <returns>The NcoreUrl object containing the built URL</returns>
    NcoreUrl GetNcoreUrl(TorrentSearchDto dto);

    /// <summary>
    /// This method creates identities for a specific torrent by searching for the torrent by id, creating a new torrent if it does not exist,
    /// linking the torrent to a user, and creating a new log entry for the torrent.
    /// </summary>
    /// <param name="dto">Id of the torrent</param>
    /// <param name="userId">Id of the user</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task CreateIdentities(SelectedTorrentDto dto, AppUser user);

    /// <summary>
    /// Retrieves a list of Torrents, sorted in descending order based on the number of associated AppUsers.
    /// </summary>
    /// <returns>A list of Torrents sorted by their number of associated AppUsers.</returns>
    /// <exception cref="Exception">Throws an exception if there's an error while fetching or sorting the torrents.</exception>
    Task<IList<Torrent>> MostActiveTorrentsByDownloadsAsync();
}
