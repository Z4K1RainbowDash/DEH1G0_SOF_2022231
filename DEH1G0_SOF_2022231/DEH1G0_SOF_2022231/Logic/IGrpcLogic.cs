using DEH1G0_SOF_2022231.Helpers;
using NcoreGrpcService.Protos;

namespace DEH1G0_SOF_2022231.Logic;

    public interface IGrpcLogic
    {
        /// <summary>
        /// Searches for torrents on a specified NcoreUrl and returns a list of TorrentDataReply.
        /// </summary>
        /// <param name="ncoreUrl">The NcoreUrl containing the url to search for torrents.</param>
        /// <returns>A list of TorrentDataReply containing the search results.</returns>
        Task<List<TorrentDataReply>> TorrentSearch(NcoreUrl ncoreUrl);

        /// <summary>
        /// Downloads a torrent file with the given id and returns it as a MemoryStream.
        /// </summary>
        /// <param name="id">The id of the torrent file to download.</param>
        /// <returns>A MemoryStream containing the downloaded torrent file.</returns>
        Task<MemoryStream> DownloadTorrent(string id);
    }
