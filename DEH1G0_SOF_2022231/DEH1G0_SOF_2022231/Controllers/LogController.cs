using DEH1G0_SOF_2022231.Data;
using DEH1G0_SOF_2022231.Models;
using Microsoft.AspNetCore.Mvc;

namespace DEH1G0_SOF_2022231.Controllers
{
    /// <summary>
    /// LogController class is a web API controller that handles TorrentLog related operations
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ITorrentLogRepository _torrentLogRepository;

        /// <summary>
        /// Constructor that initializes the LogController class with a ITorrentLogRepository
        /// </summary>
        /// <param name="torrentLogRepository">ITorrentLogRepository interface for handling TorrentLog related operations</param>
        public LogController(ITorrentLogRepository torrentLogRepository)
        {
            this._torrentLogRepository = torrentLogRepository ?? throw new ArgumentNullException(nameof(torrentLogRepository));
        }

        /// <summary>
        /// GetLogs is an HTTP GET method that returns a list of all TorrentLogs
        /// </summary>
        /// <returns>
        /// <see cref="OkResult"/> with torrents.
        /// <see cref="StatusCodeResult"/> with a status code of 500 (Internal Server Error) if an error occurs while getting torrents.
        /// </returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TorrentLog>>> GetLogs()
        {
            try
            {
                var torrents = await this._torrentLogRepository.GetAllAsync();
                return Ok(torrents);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"An error occurred while retrieving the list of torrents: {ex.Message}");
            }
        }

    }

}
