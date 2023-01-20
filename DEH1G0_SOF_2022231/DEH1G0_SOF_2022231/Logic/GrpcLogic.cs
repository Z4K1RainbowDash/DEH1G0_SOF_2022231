using DEH1G0_SOF_2022231.Helpers;
using Grpc.Core;
using Grpc.Net.Client;
using NcoreGrpcService.Protos;

namespace DEH1G0_SOF_2022231.Logic
{
    public class GrpcLogic
    {
        private string _serverAddress;
        private Ncore.NcoreClient _client;

        public GrpcLogic(string serverAddress)
        {
            this._serverAddress = serverAddress ?? throw new ArgumentNullException(nameof(serverAddress));
            GrpcChannel channel = GrpcChannel.ForAddress(_serverAddress);
            this._client = new Ncore.NcoreClient(channel);

        }

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

    }
}
