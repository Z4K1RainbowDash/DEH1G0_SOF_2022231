using NcoreGrpcService.Protos;

namespace NcoreGrpcService.Logic
{
    public interface INcoreWebScraping
    {
        byte[]? DownloadTorrent(string id);
        List<TorrentDataReply> Searching(SearchRequest request);
    }
}