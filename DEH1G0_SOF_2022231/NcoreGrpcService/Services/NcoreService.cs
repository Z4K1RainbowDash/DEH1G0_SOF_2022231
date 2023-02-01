using Grpc.Core;
using NcoreGrpcService.Logic;
using NcoreGrpcService.Protos;

namespace NcoreGrpcService.Services
{
    /// <summary>
    /// NcoreService is a class that implements the NcoreBase abstract class and serves as a service to search and download torrents.
    /// </summary>
    public class NcoreService:Ncore.NcoreBase
    {
        private readonly ILogger<NcoreService> _logger;
        private readonly INcoreWebScraping _webScraping;

        /// <summary>
        /// Initializes a new instance of the NcoreService class.
        /// </summary>
        /// <param name="logger">The ILogger interface for logging.</param>
        /// <param name="webScraping">The INcoreWebScraping interface for web scraping functions.</param>
        public NcoreService(ILogger<NcoreService> logger, INcoreWebScraping webScraping)
        {
            _logger = logger;
            _webScraping = webScraping;
        }

        /// <summary>
        /// Implements the abstract TorrentDownload method from the NcoreBase class.
        /// Downloads the torrent file and sends the data in chunks to the response stream.
        /// </summary>
        /// <param name="request">The TorrentRequest object that contains the torrent ID to be downloaded.</param>
        /// <param name="responseStream">The IServerStreamWriter interface for sending the torrent data in chunks.</param>
        /// <param name="context">The ServerCallContext object that contains the context for the gRPC call.</param>

        public override async Task TorrentDownload(TorrentRequest request, IServerStreamWriter<TorrentFileResponse> responseStream, ServerCallContext context)
        {
            byte[]? bb = this._webScraping.DownloadTorrent(request.Id);
            this._logger.LogInformation("Torrent with ID {request.Id} has been downloaded.", request.Id);

            // max file size: 4 MB
            await responseStream.WriteAsync(new TorrentFileResponse { DataChunk = Google.Protobuf.ByteString.CopyFrom(bb) });
            this._logger.LogInformation("Torrent with ID {request.Id} has been sent to the stream.", request.Id);
        }

        /// <summary>
        /// Implements the abstract TorrentSearch method from the NcoreBase class.
        /// Searches for torrents and sends the search results in chunks to the response stream.
        /// </summary>
        /// <param name="request">The SearchRequest object that contains the search query.</param>
        /// <param name="responseStream">The IServerStreamWriter interface for sending the search results in chunks.</param>
        /// <param name="context">The ServerCallContext object that contains the context for the gRPC call.</param>
        public override async Task TorrentSearch(SearchRequest request, IServerStreamWriter<TorrentDataReply> responseStream, ServerCallContext context)
        {
            this._logger.LogInformation("Searching for torrents with URL: {0}", request.Url);
            var searchResults = this._webScraping.Searching(request);
            
            foreach (TorrentDataReply r in searchResults)
            {
                await responseStream.WriteAsync(r);
            }
        }

    }
}
