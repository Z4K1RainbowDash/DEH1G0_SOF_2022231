using HtmlAgilityPack;
using NcoreGrpcService.Protos;
using RestSharp;

namespace NcoreGrpcService.Logic
{
    public class NcoreWebScraping: INcoreWebScraping
    {
        private readonly IConfiguration _configuration;
        // TODO: readonly?
        private RestClient client = new RestClient("https://ncore.pro"); 

        private readonly List<KeyValuePair<string, string>> defaultHeaders = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("Accept" , "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9" ),
                new KeyValuePair<string, string>("Accept-Encoding" , "gzip, deflate, br" ),
                new KeyValuePair<string, string>("Accept-Language" , "hu-HU,hu;q=0.9,en-US;q=0.8,en;q=0.7"),
                new KeyValuePair<string, string>("Cache-Control" , "max-age=0"),
                new KeyValuePair<string, string>("Connection" , "keep-alive"),
                new KeyValuePair<string, string>("Content-Type" , "application/x-www-form-urlencoded"),
                new KeyValuePair<string, string>("sec-ch-ua" , "\"Chromium\";v=\"102\", \"Opera\";v=\"88\", \";Not A Brand\";v=\"99\""),
                new KeyValuePair<string, string>("sec-ch-ua-platform" , "\"Windows\""),
                new KeyValuePair<string, string>("sec-ch-ua-mobile" , "?0"),
                new KeyValuePair<string, string>("Sec-Fetch-Site" , "same-origin"),
                new KeyValuePair<string, string>("Sec-Fetch-Mode" , "navigate"),
                new KeyValuePair<string, string>("Sec-Fetch-Dest" , "document"),
                new KeyValuePair<string, string>("Sec-Fetch-User" , " ?1"),
                new KeyValuePair<string, string>("Host" , "ncore.pro"),
                new KeyValuePair<string, string>("User-Agent" , "Mozilla /5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/102.0.5005.115 Safari/537.36 OPR/88.0.4412.53"),
                new KeyValuePair<string, string>("Upgrade-Insecure-Requests" , "1")
            };

        private readonly string _ncoreUsername; 
        private readonly string _ncorePassword;
        private readonly string _ncoreKey;

        public NcoreWebScraping(IConfiguration configuration)
        {
            this._configuration = configuration;
            this._ncoreUsername = this._configuration.GetConnectionString("NcoreUsername");
            this._ncorePassword = this._configuration.GetConnectionString("NcorePassword");
            this._ncoreKey = this._configuration.GetConnectionString("NcoreKey");

        }

        private void Login()
        {

            var req = new RestRequest("login.php", Method.Post);
            
            foreach (var item in defaultHeaders)
            {
                req.AddHeader(item.Key, item.Value);
            }

            string urlParameters = $"set_lang=hu&submitted=1" +
                $"&nev={this._ncoreUsername}" +
                $"&pass={this._ncorePassword}";

            req.AddParameter("application/x-www-form-urlencoded",urlParameters , ParameterType.RequestBody);

            var response = client.Execute(req);

            var indexReq = new RestRequest("index.php", Method.Get);

            var indexPage = client.Execute(indexReq);

        }

        private RestResponse GetResponse(string url)
        {
            var req = new RestRequest(url, Method.Get);
            req.AddHeaders(defaultHeaders);
            return client.Execute(req);
        }

        public List<TorrentDataReply> Searching(SearchRequest request)
        {
            
            this.Login();
            return GetTorrents(request.Url);

        }


        private List<TorrentDataReply> GetTorrents(string url)
        {
            List<TorrentDataReply> torrents = new List<TorrentDataReply>();


            var links = new List<string>();
            links.Add(url);

            for (int i = 0; i < links.Count; i++)
            {
                var response = GetResponse(links[i]);
                if (response.Content != null)
                {
                    torrents.AddRange(HtmlScrapping(response.Content));
                    FindOtherPages(response.Content, links);
                }
            }

            return torrents;

        }

        private List<TorrentDataReply> HtmlScrapping(string content)
        {
            List<TorrentDataReply> torrentDatas = new List<TorrentDataReply>();
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(content);
            var torrents = htmlDoc.DocumentNode.SelectNodes("//div[@class='box_torrent_all']/div[@class='box_torrent']").ToList();

            var idNodeList = htmlDoc.DocumentNode.SelectNodes("//div[@class='torrent_lenyilo' or @class='torrent_lenyilo2']").ToList();

            int i = 0;
            foreach (var torrent in torrents)
            {
                string torrentName = torrent.SelectSingleNode(".//div/div/div/div/a").Attributes["title"].Value;
                string torrentDate = torrent.SelectSingleNode(".//div[@class='box_feltoltve2']").InnerHtml.Replace("<br>", " ");
                string torrentSize = torrent.SelectSingleNode(".//div[@class='box_meret2']").InnerText;
                string torrentDownloads = torrent.SelectSingleNode(".//div[@class='box_d2']").InnerText;
                string torrentSeeders = torrent.SelectSingleNode(".//div[@class='box_s2']").InnerText;
                string torrentLeechers = torrent.SelectSingleNode(".//div[@class='box_l2']").InnerText;
                string torrentId = idNodeList[i].Attributes["id"].Value;
                #region torrent mini image
                string torrentImageIconLink;
                try
                {
                    //System.NullReferenceException: 'Object reference not set to an instance of an object.'
                    torrentImageIconLink = torrent.SelectSingleNode(".//div[@class='infobar']/img").Attributes["onmouseover"].Value;
                }
                catch (Exception)
                {
                    torrentImageIconLink = string.Empty;
                }
                #endregion

                torrentDatas.Add(new TorrentDataReply
                {
                    Name = torrentName,
                    Image = torrentImageIconLink,
                    Date = torrentDate,
                    Size = torrentSize,
                    Downloads = torrentDownloads,
                    Seeders = torrentSeeders,
                    Leechers = torrentLeechers,
                    Id = torrentId
                });
                i++;
            }


            return torrentDatas;
        }

        private void FindOtherPages(string htmlPage, List<string> links)
        {

            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(htmlPage);

            var nodes = htmlDoc.DocumentNode.SelectNodes("//div[@id='pager_bottom']/a");

            if (nodes is not null)
            {
                foreach (var item in nodes)
                {
                    if (!links.Contains(item.Attributes["href"].Value))
                    {
                        links.Add(item.Attributes["href"].Value);
                    }
                }
            }
        }

        public byte[]? DownloadTorrent(string id)
        {
            string link = $"https://ncore.pro/torrents.php?action=download&id={id}" +
                $"&key={this._ncoreKey}";

            var request = new RestRequest(link, Method.Get);
            var response = client.DownloadData(request);

            return response;
        }
    }
}
