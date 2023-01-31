using NcoreGrpcService.Protos;

namespace NcoreGrpcService.Logic
{
    /// <summary>
    /// Interface for web scraping operations on Ncore.
    /// </summary>
    public interface INcoreWebScraping
    {
        /// <summary>
        /// Downloads a torrent file for the given torrent ID.
        /// </summary>
        /// <param name="id">The ID of the torrent to download.</param>
        /// <returns>A byte array representing the torrent file, or null if the download failed.</returns>
        byte[]? DownloadTorrent(string id);

        /// <summary>
        /// Performs a search using the specified <see cref="SearchRequest"/> and logs in if necessary.
        /// </summary>
        /// <param name="request">The search request to perform.</param>
        /// <returns>A list of <see cref="TorrentDataReply"/> objects representing the search results.</returns>
        List<TorrentDataReply> Searching(SearchRequest request);
    }
}