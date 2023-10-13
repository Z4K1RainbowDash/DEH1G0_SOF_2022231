using DEH1G0_SOF_2022231.Helpers;
using DEH1G0_SOF_2022231.Models.Helpers;

namespace DEH1G0_SOF_2022231.Logic;

    /// <summary>
    /// Contains the logic interface for building Ncore Url
    /// </summary>
    public interface ITorrentLogic
    {
        /// <summary>
        /// Returns the NcoreUrl based on the input search text and categories.
        /// </summary>
        /// <param name="searchText">Text used for searching.</param>
        /// <param name="movies">An object of Movies class that contains the selected movie categories</param>
        /// <param name="series">An object of Series class that contains the selected series categories</param>
        /// <param name="musics">An object of Music class that contains the selected music categories</param>
        /// <param name="programs">An object of Programs class that contains the selected program categories</param>
        /// <param name="games">An object of Games class that contains the selected game categories</param>
        /// <param name="books">An object of Books class that contains the selected book categories</param>
        /// <returns>The NcoreUrl object containing the built URL</returns>
        NcoreUrl GetNcoreUrl(string searchText, Movies movies, Series series, Music musics, Programs programs, Games games, Books books);

        /// <summary>
        /// This method creates identities for a specific torrent by searching for the torrent by id, creating a new torrent if it does not exist,
        /// linking the torrent to a user, and creating a new log entry for the torrent.
        /// </summary>
        /// <param name="torrentId">Id of the torrent</param>
        /// <param name="torrentName">Name of the torrent</param>
        /// <param name="userId">Id of the user</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task CreateIdentities(string torrentId, string torrentName, string userId);
    }
