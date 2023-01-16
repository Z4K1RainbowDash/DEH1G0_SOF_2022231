using Grpc.Core;
using NcoreGrpcService.Logic;
using NcoreGrpcService.Protos;

namespace NcoreGrpcService.Services
{
    public class NcoreService:Ncore.NcoreBase
    {
        private readonly ILogger<NcoreService> _logger;
        private readonly INcoreWebScraping _webScraping;

        public NcoreService(ILogger<NcoreService> logger, INcoreWebScraping webScraping)
        {
            _logger = logger;
            _webScraping = webScraping;
        }

        public override async Task TorrentDownload(TorrentRequest request, IServerStreamWriter<TorrentFileResponse> responseStream, ServerCallContext context)
        {
            byte[]? bb = this._webScraping.DownloadTorrent(request.Id);

            // max file size: 4 MB
            await responseStream.WriteAsync(new TorrentFileResponse { DataChunk = Google.Protobuf.ByteString.CopyFrom(bb) });
        }

        public override async Task TorrentSearch(SearchRequest request, IServerStreamWriter<TorrentDataReply> responseStream, ServerCallContext context)
        {
            var searchResults = this._webScraping.Searching(request);

            foreach (TorrentDataReply r in searchResults)
            {
                await responseStream.WriteAsync(r);
            }
        }

    }
}
