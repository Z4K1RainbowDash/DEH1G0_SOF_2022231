using DEH1G0_SOF_2022231.Data;
using DEH1G0_SOF_2022231.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DEH1G0_SOF_2022231.Controllers
{
    /// <summary>
    /// LogController class is a web API controller that handles TorrentLog related operations
    /// </summary>
    [Route("api/[controller]")]
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
        /// <returns>IEnumerable<TorrentLog> - List of all TorrentLogs</returns>
        [HttpGet]
        public async Task<IEnumerable<TorrentLog>> GetLogs()
        {
            return await this._torrentLogRepository.GetAllAsync();
        }

    }

}
