using DEH1G0_SOF_2022231.Data;
using DEH1G0_SOF_2022231.Logic;
using DEH1G0_SOF_2022231.Models;
using DEH1G0_SOF_2022231.Models.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace DEH1G0_SOF_2022231.Controllers
{
    public class TorrentsController : Controller
    {
        private readonly ITorrentLogic _torrentLogic;
        private readonly IGrpcLogic _grpcLogic;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITorrentRepository _torrentRepository;
        private readonly ITorrentLogRepository _torrentLogRepository;
        private readonly IAppUserRepository _appUserRepository;

        public TorrentsController(ITorrentLogic torrentLogic, IGrpcLogic grpcLogic, ITorrentRepository torrentRepository, UserManager<AppUser> userManager, ITorrentLogRepository torrentLogRepository, IAppUserRepository appUserRepository)
        {
            _torrentLogic = torrentLogic;
            _grpcLogic = grpcLogic;
            _torrentRepository = torrentRepository;
            _userManager = userManager;
            _torrentLogRepository = torrentLogRepository;
            _appUserRepository = appUserRepository;
        }

        [Authorize]
        public IActionResult SearchTorrent()
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
        [Authorize]
        public async Task<IActionResult> SearchTorrent(TorrentsViewModel vm)
        {
            var url = this._torrentLogic.GetNcoreUrl(vm.SearchText, vm.Movies, vm.Series, vm.Musics, vm.Programs, vm.Games, vm.Books);
            var torrents = await this._grpcLogic.TorrentSearch(url);
            vm.Torrents = torrents;
                
            return View(vm);

        }

        [Authorize]
        public async Task<IActionResult> DownloadTorrent(string torrentId, string name)
        {
            string realName = name.Replace('_', ' ');
            var userId = this._userManager.GetUserId(this.User);

            await this._torrentLogic.CreateIdentities(torrentId, realName, userId);
          


            var memoryStream = await this._grpcLogic.DownloadTorrent(torrentId);
            string torrentName = name + ".torrent";

            
            return File(memoryStream, "application/octet-stream", torrentName);

        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ListLogs()
        {

            return View(await this._torrentLogRepository.GetAllAsync());
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> MostActiveUsersByDownloads()
        {
            var users = await _appUserRepository.GetAllAsync();
            var sortedUsers = users.OrderByDescending(u => u.Torrents.Count);
            return View(sortedUsers);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> MostPopularTorrents()
        {
            var torrent = await this._torrentRepository.GetAllAsync();
            var sortedTorrents = torrent.OrderByDescending(u => u.AppUsers.Count);

            return View(sortedTorrents);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> TorrentUsersByTorrent(string torrentId)
        {
            var users = await this._torrentRepository.GetUsersByTorrentId(torrentId);

            var torrent = await this._torrentRepository.GetByIdAsync(torrentId);

            TorrentUsersViewModel vm = new TorrentUsersViewModel()
            {
                TorrentName = torrent.Name,
                AppUsers = users.ToList()

            };
            return View(vm);
        }



    }
}
