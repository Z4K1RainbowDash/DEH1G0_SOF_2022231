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
    /// <summary>
    /// Controller for handling Torrent related actions, such as searching, downloading, and listing logs.
    /// </summary>
    public class TorrentsController : Controller
    {
        private readonly ITorrentLogic _torrentLogic;
        private readonly IGrpcLogic _grpcLogic;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITorrentRepository _torrentRepository;
        private readonly ITorrentLogRepository _torrentLogRepository;
        private readonly IAppUserRepository _appUserRepository;


        /// <summary>
        /// Initializes a new instance of the <see cref="TorrentsController"/> class.
        /// </summary>
        /// <param name="torrentLogic">The logic for handling torrent related actions.</param>
        /// <param name="grpcLogic">The logic for communicating with the gRPC service.</param>
        /// <param name="torrentRepository">The repository for accessing torrent related data.</param>
        /// <param name="userManager">The manager for handling user related actions.</param>
        /// <param name="torrentLogRepository">The repository for accessing torrent log data.</param>
        /// <param name="appUserRepository">The repository for accessing user related data.</param>
        public TorrentsController(ITorrentLogic torrentLogic, IGrpcLogic grpcLogic, ITorrentRepository torrentRepository, UserManager<AppUser> userManager, ITorrentLogRepository torrentLogRepository, IAppUserRepository appUserRepository)
        {
            _torrentLogic = torrentLogic;
            _grpcLogic = grpcLogic;
            _torrentRepository = torrentRepository;
            _userManager = userManager;
            _torrentLogRepository = torrentLogRepository;
            _appUserRepository = appUserRepository;
        }

        /// <summary>
        /// Method that handles the GET request to search for torrents.
        /// </summary>
        /// <returns>A view displaying the search form.</returns>
        [Authorize]
        public IActionResult SearchTorrent()
        {
            TorrentsViewModel vm = new TorrentsViewModel();


           
            
            return View(vm);
            
        }

        /// <summary>
        /// Method that handles the POST request to search for torrents.
        /// </summary>
        /// <param name="vm">The view model containing the search parameters.</param>
        /// <returns>A view displaying the search form and the search results (if any).</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SearchTorrent(TorrentsViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var url = this._torrentLogic.GetNcoreUrl(vm.SearchText, vm.Movies, vm.Series, vm.Musics, vm.Programs, vm.Games, vm.Books);
            var torrents = await this._grpcLogic.TorrentSearch(url);
            vm.Torrents = torrents;
                
            return View(vm);

        }

        /// <summary>
        /// Downloads the torrent with the specified ID.
        /// </summary>
        /// <param name="torrentId">The ID of the torrent to download.</param>
        /// <param name="name">The name of the torrent file to download.</param>
        /// <returns>The torrent file in `application/octet-stream` format.</returns>
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

        /// <summary>
        /// Returns a list of all logs. Accessible only to users with the "Admin" role.
        /// </summary>
        /// <returns>A list of logs.</returns>
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ListLogs()
        {

            return View(await this._torrentLogRepository.GetAllAsync());
        }

        /// <summary>
        /// Returns a list of the most active users based on the number of downloads. Accessible only to users with the "Admin" role.
        /// </summary>
        /// <returns>A list of the most active users.</returns>
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> MostActiveUsersByDownloads()
        {
            var users = await _appUserRepository.GetAllAsync();
            var sortedUsers = users.OrderByDescending(u => u.Torrents.Count);
            return View(sortedUsers);
        }

        /// <summary>
        /// Returns a list of the most popular torrents based on the number of downloads.
        /// </summary>
        /// <returns>A list of the most popular torrents.</returns>
        [Authorize]
        public async Task<IActionResult> MostPopularTorrents()
        {
            var torrent = await this._torrentRepository.GetAllAsync();
            var sortedTorrents = torrent.OrderByDescending(u => u.AppUsers.Count);

            return View(sortedTorrents);
        }

        /// <summary>
        /// Returns a list of users who have downloaded a specific torrent. Accessible only to users with the "Admin" role.
        /// </summary>
        /// <param name="torrentId">The ID of the torrent.</param>
        /// <returns>A list of users who have downloaded the specified torrent.</returns>
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
