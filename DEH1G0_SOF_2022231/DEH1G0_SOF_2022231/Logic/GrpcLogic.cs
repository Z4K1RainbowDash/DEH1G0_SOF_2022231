using DEH1G0_SOF_2022231.Helpers;
using Grpc.Core;
using Grpc.Net.Client;
using NcoreGrpcService.Protos;

namespace DEH1G0_SOF_2022231.Logic
{
    /// <summary>
    /// This class handles the communication with the gRPC server.
    /// </summary>
    public class GrpcLogic : IGrpcLogic
    {
        private string _serverAddress;
        private Ncore.NcoreClient _client;

        /// <summary>
        ///  Initializes a new instance of the <see cref="GrpcLogic"/> class.
        /// </summary>
        /// <param name="serverAddress">The address of the gRPC server to connect to.</param>
        /// <exception cref="ArgumentNullException">Thrown if the serverAddress parameter is null</exception>
        public GrpcLogic(string serverAddress)
        {
            this._serverAddress = serverAddress ?? throw new ArgumentNullException(nameof(serverAddress));
            GrpcChannel channel = GrpcChannel.ForAddress(_serverAddress);
            this._client = new Ncore.NcoreClient(channel);

        }

        /// <summary>
        /// Searches for torrents on a specified NcoreUrl and returns a list of TorrentDataReply.
        /// </summary>
        /// <param name="ncoreUrl">The NcoreUrl containing the url to search for torrents.</param>
        /// <returns>A list of TorrentDataReply containing the search results.</returns>
        /// <exception cref="RpcException">Thrown when the search is cancelled.</exception>
        public async Task<List<TorrentDataReply>> TorrentSearch(NcoreUrl ncoreUrl)
        {
            List<TorrentDataReply> torrents = new List<TorrentDataReply>();
            using var ncoreReply = this._client.TorrentSearch(
                new SearchRequest { Url = ncoreUrl.Url });

            try
            {
                await foreach (TorrentDataReply torrent in ncoreReply.ResponseStream.ReadAllAsync())
                {
                    torrents.Add(torrent);
                }
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled)
            {
                // TODO: logger
            }

            return torrents;
        }

        /// <summary>
        /// Downloads a torrent file with the given id and returns it as a MemoryStream.
        /// </summary>
        /// <param name="id">The id of the torrent file to download.</param>
        /// <returns>A MemoryStream containing the downloaded torrent file.</returns>
        public async Task<MemoryStream> DownloadTorrent(string id)
        {
            var request = new TorrentRequest { Id = id };
            var memoryStream = new MemoryStream();

            
            // get file from stream
            using (var responseStream = this._client.TorrentDownload(request))
            {
                while (await responseStream.ResponseStream.MoveNext())
                {
                    var torrentFileResponse = responseStream.ResponseStream.Current;
                    await memoryStream.WriteAsync(torrentFileResponse.DataChunk.ToByteArray());
                }
            }

            memoryStream.Position = 0;

            return memoryStream;
        }

    }
}
