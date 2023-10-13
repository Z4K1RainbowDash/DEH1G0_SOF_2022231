using DEH1G0_SOF_2022231.Data;
using DEH1G0_SOF_2022231.Logic;
using DEH1G0_SOF_2022231.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DEH1G0_SOF_2022231.Models.DTOs;
using NcoreGrpcService.Protos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DEH1G0_SOF_2022231.Controllers;

/// <summary>
/// Controller for handling Torrent related actions, such as searching, downloading, and listing logs.
/// </summary>
[Route("api/[controller]/[action]")]
[ApiController]
public class TorrentsController : ControllerBase
{
    private readonly ITorrentLogic _torrentLogic;
    private readonly IGrpcLogic _grpcLogic;
    private readonly ITorrentRepository _torrentRepository;
    private readonly ITorrentLogRepository _torrentLogRepository;
    private readonly UserManager<AppUser> _userManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="TorrentsController"/> class.
    /// </summary>
    /// <param name="torrentLogic">The logic for handling torrent related actions.</param>
    /// <param name="grpcLogic">The logic for communicating with the gRPC service.</param>
    /// <param name="torrentRepository">The repository for accessing torrent related data.</param>
    /// <param name="torrentLogRepository">The repository for accessing torrent log data.</param>
    /// <param name="userManager">The repository for accessing user related data.</param>
    public TorrentsController(ITorrentLogic torrentLogic, IGrpcLogic grpcLogic, ITorrentRepository torrentRepository, ITorrentLogRepository torrentLogRepository, UserManager<AppUser> userManager)
    {
        this._torrentLogic = torrentLogic;
        this._grpcLogic = grpcLogic;
        this._torrentRepository = torrentRepository;
        this._torrentLogRepository = torrentLogRepository;
        this._userManager = userManager;
    }

    /// <summary>
    /// Method that handles the POST request to search for torrents.
    /// </summary>
    /// <param name="dto">DTO that contains the selected torrent categories and the search text.</param>
    /// <returns>
    /// <see cref="OkResult"/> containing the <see cref="TorrentDataReply"/> List.
    /// <see cref="BadRequestResult"/> if the <see cref="TorrentSearchDto"/> is not valid.
    /// <see cref="StatusCodeResult"/> with a status code of 500 (Internal Server Error) if an error occurs while searching torrents.
    /// </returns>
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<List<TorrentDataReply>>> SearchTorrent(TorrentSearchDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var url = this._torrentLogic.GetNcoreUrl(dto);
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
    /// <param name="dto">DTO that contains the selected torrent.</param>
    /// <returns>
    /// <see cref="FileResult"/> containing the torrent file in `application/octet-stream` format.
    /// <see cref="BadRequestResult"/> if the torrentId or name parameter is null or empty .
    /// <see cref="StatusCodeResult"/> with a status code of 500 (Internal Server Error) if an error occurs while deleting the user.
    /// </returns>
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> DownloadTorrent(SelectedTorrentDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            AppUser? user = await this._userManager.FindByIdAsync(this._userManager.GetUserId(this.User));

            if (user == null)
            {
                throw new NullReferenceException("UserId not found");
            }
            
            await this._torrentLogic.CreateIdentities(dto, user);

            var memoryStream = await this._grpcLogic.DownloadTorrent(dto.TorrentId);
            string torrentName = dto.TorrentName.Replace('_',' ') + ".torrent";     
            
            if (memoryStream.Length != 0)
            {
                return File(memoryStream, "application/octet-stream", torrentName);
            }
            
            return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while downloading torrent: MemoryStream Length is 0!");
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
            var users = this._userManager.Users;
            var sortedUsers = await users.OrderByDescending(u => u.Torrents.Count).ToListAsync();
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
    public async Task<ActionResult<IEnumerable<Torrent>>> MostPopularTorrents()
    {
        try
        {
            var sortedTorrents = await this._torrentLogic.MostActiveTorrentsByDownloadsAsync();

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
    /// <see cref="OkResult"/> containing the <see cref="TorrentUsersDto"/>.
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

            if (torrent == null)
            {
                throw new NullReferenceException("Torrent not found");
            }
            TorrentUsersDto dto = new TorrentUsersDto()
            {
                TorrentName = torrent.Name,
                AppUsers = users.ToList()
            };
            return Ok(dto);
        }
        catch(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                $"An error occurred while getting torrent with users: {ex.Message}");
        }
    }
}

