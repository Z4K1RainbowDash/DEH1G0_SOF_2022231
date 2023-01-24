using DEH1G0_SOF_2022231.Data;
using DEH1G0_SOF_2022231.Logic;
using DEH1G0_SOF_2022231.Models;
using DEH1G0_SOF_2022231.Models.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace DEH1G0_SOF_2022231.Controllers
{
    public class TorrentsController : Controller
    {
        private readonly ITorrentLogic _torrentLogic;
        private readonly IGrpcLogic _grpcLogic;

        public TorrentsController(ITorrentLogic torrentLogic, IGrpcLogic grpcLogic)
        {
            _torrentLogic = torrentLogic;
            _grpcLogic = grpcLogic;
        }

        public async Task<IActionResult> SearchTorrent()
        {
            TorrentsViewModel vm = new TorrentsViewModel();


            /* Testing data
            List<Torrent> t = new List<Torrent>
            {
                new Torrent {
                    Name = "N1",
                    ImageUrl = "asd",
                    Size= "1 gb",
                    Downloads= "100",
                    Seeders = 10,
                    Leechers = 10

                },

                new Torrent {
                    Name = "N2",
                    ImageUrl = "asd2",
                    Size= "12 gb",
                    Downloads= "100",
                    Seeders = 10,
                    Leechers = 10

                }
            };
            

            vm.Torrents = t;*/
            
            return View(vm);
            
        }

        [HttpPost]
        public async Task<IActionResult> SearchTorrent(TorrentsViewModel vm)
        {
            var url = this._torrentLogic.GetNcoreUrl(vm.SearchText, vm.Movies, vm.Series, vm.Musics, vm.Programs, vm.Games, vm.Books);
            var torrents = await this._grpcLogic.TorrentSearch(url);
            vm.Torrents = torrents
                .Select(
                x => new Torrent { 
                    NcoreId = int.Parse(x.Id),  
                    Name = x.Name,
                    ImageUrl = x.Image,
                    CreatedDateTime = x.Date,
                    Size = x.Size,
                    Downloads = x.Downloads,
                    Seeders = int.Parse(x.Seeders),
                    Leechers = int.Parse(x.Leechers)

                }).ToList();
            return View(vm);

        }

        public async Task<IActionResult> DownloadTorrent(string id, string name)
        {
            
            var memoryStream = await this._grpcLogic.DownloadTorrent(id);
            string torrentName = name + ".torrent";

            
            return File(memoryStream, "application/octet-stream", torrentName);

        }


    }
}
