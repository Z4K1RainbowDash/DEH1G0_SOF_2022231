using DEH1G0_SOF_2022231.Data;
using DEH1G0_SOF_2022231.Logic;
using DEH1G0_SOF_2022231.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using DEH1G0_SOF_2022231.Models.DTOs;
using NcoreGrpcService.Protos;

namespace DEH1G0_SOF_2022231.Controllers
{
    /// <summary>
    /// Controller for handling Torrent related actions, such as searching, downloading, and listing logs.
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TorrentsController : ControllerBase
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
        /// Method that handles the POST request to search for torrents.
        /// </summary>
        /// <param name="torrentSearchDTO">The view model containing the search parameters.</param>
        /// <returns>
        /// <see cref="OkResult"/> containing the <see cref="TorrentDataReply"/> List.
        /// <see cref="BadRequestResult"/> if the <see cref="TorrentSearchDTO"/> is not valid.
        /// <see cref="StatusCodeResult"/> with a status code of 500 (Internal Server Error) if an error occurs while searching torrents.
        /// </returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<List<TorrentDataReply>>> SearchTorrent(TorrentSearchDTO torrentSearchDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var url = this._torrentLogic.GetNcoreUrl(torrentSearchDTO.SearchText, torrentSearchDTO.Movies, torrentSearchDTO.Series, torrentSearchDTO.Music, torrentSearchDTO.Programs, torrentSearchDTO.Games, torrentSearchDTO.Books);
                var torrents = await this._grpcLogic.TorrentSearch(url);
                return Ok(torrents);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"An error occurred while searching torrents: {ex.Message}");
            }
        }

        /// <summary>
        /// Downloads the torrent with the specified ID and Name.
        /// </summary>
        /// <param name="torrentId">The ID of the torrent to download.</param>
        /// <param name="name">The name of the torrent file to download.</param>
        /// <returns>
        /// <see cref="FileResult"/> containing the torrent file in `application/octet-stream` format.
        /// <see cref="BadRequestResult"/> if the torrentId or name parameter is null or empty .
        /// <see cref="StatusCodeResult"/> with a status code of 500 (Internal Server Error) if an error occurs while deleting the user.
        /// </returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> DownloadTorrent(string torrentId, string name)
        {
            // TODO HTTP 404
            if (torrentId == null || name == null || torrentId == string.Empty || name == string.Empty)
            {
                return BadRequest(); // TODO : ModelBinding
            }
            try
            {
                string torrentNameWithoutUnderscore = name.Replace('_', ' ');
                string userId = this._userManager.GetUserId(this.User);
                
                await this._torrentLogic.CreateIdentities(torrentId, torrentNameWithoutUnderscore, userId);

                var memoryStream = await this._grpcLogic.DownloadTorrent(torrentId);
                string torrentName = name + ".torrent";     

    
                if (memoryStream.Length != 0)
                {
                    return File(memoryStream, "application/octet-stream", torrentName);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while downloading torrent: MemoryStream Length is 0!"); 
                }
            }
            catch(Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"An error occurred while searching torrents: {ex.Message}");
            }

        }

        /// <summary>
        /// Returns a list of all <see cref="TorrentLog"/>s. Accessible only to users with the "Admin" role.
        /// </summary>
        /// <returns>
        /// <see cref="OkResult"/> containing the <see cref="TorrentLog"/>s.
        /// <see cref="StatusCodeResult"/> with a status code of 500 (Internal Server Error) if an error occurs while getting <see cref="TorrentLog"/>s.
        /// </returns>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TorrentLog>>> GetTorrentLogs()
        {
            try
            {
                var torrentLogs = await this._torrentLogRepository.GetAllAsync();
                return Ok(torrentLogs);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"An error occurred while getting TorrentLogs: {ex.Message}");
            }
        }

        /// <summary>
        /// Returns a list of the most active users based on the number of downloads. Accessible only to users with the "Admin" role.
        /// </summary>
        /// <returns>A list of the most active users.
        /// <see cref="OkResult"/> containing the most active <see cref="AppUser"/>s.
        /// <see cref="StatusCodeResult"/> with a status code of 500 (Internal Server Error) if an error occurs while getting <see cref="AppUser"/>s.
        /// </returns>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> MostActiveUsersByDownloads()
        {
            try
            {
                var users = await _appUserRepository.GetAllAsync();
                var sortedUsers = users.OrderByDescending(u => u.Torrents.Count);
                return Ok(sortedUsers);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"An error occurred while getting AppUsers: {ex.Message}");
            }
        }

        /// <summary>
        /// Returns a list of the most popular torrents based on the number of downloads.
        /// </summary>
        /// <returns>
        /// <see cref="OkResult"/> containing the popular <see cref="Torrent"/>s.
        /// <see cref="StatusCodeResult"/> with a status code of 500 (Internal Server Error) if an error occurs while getting <see cref="Torrent"/>s.
        /// </returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> MostPopularTorrents()
        {
            try
            {
                var torrent = await this._torrentRepository.GetAllAsync();
                var sortedTorrents = torrent.OrderByDescending(u => u.AppUsers.Count);

                return Ok(sortedTorrents);
            }
            catch(Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"An error occurred while getting torrents: {ex.Message}");
            }
        }

        /// <summary>
        /// Returns a list of users who have downloaded a specific torrent. Accessible only to users with the "Admin" role.
        /// </summary>
        /// <param name="torrentId">The ID of the torrent.</param>
        /// <returns>
        /// <see cref="OkResult"/> containing the <see cref="TorrentUsersViewModel"/>.
        /// <see cref="StatusCodeResult"/> with a status code of 500 (Internal Server Error) if an error occurs while getting <see cref="Torrent"/>s.
        /// </returns>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> TorrentUsersByTorrent(string torrentId)
        {
            try
            {
                var users = await this._torrentRepository.GetUsersByTorrentId(torrentId);

                var torrent = await this._torrentRepository.GetByIdAsync(torrentId);

                TorrentUsersViewModel vm = new TorrentUsersViewModel()
                {
                    TorrentName = torrent.Name,
                    AppUsers = users.ToList()

                };
                return Ok(vm);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"An error occurred while getting torrent with users: {ex.Message}");
            }
        }



    }
}
