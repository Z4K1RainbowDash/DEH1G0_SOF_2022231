using DEH1G0_SOF_2022231.Helpers;
using NcoreGrpcService.Protos;

namespace DEH1G0_SOF_2022231.Logic
{
    public interface IGrpcLogic
    {
        Task<List<TorrentDataReply>> TorrentSearch(NcoreUrl ncoreUrl);
    }
}