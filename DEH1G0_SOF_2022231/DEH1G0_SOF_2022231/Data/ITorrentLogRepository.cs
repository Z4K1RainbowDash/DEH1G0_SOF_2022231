using DEH1G0_SOF_2022231.Models;

namespace DEH1G0_SOF_2022231.Data;

    /// <summary>
    /// Repository interface for TorrentLog entities
    /// </summary>
    public interface ITorrentLogRepository : IGenericRepository<TorrentLog>
    {
        /// <summary>
        /// Asynchronously deletes the <see cref="TorrentLog"/> with the given ID.
        /// </summary>
        /// <param name="id">The ID of the <see cref="TorrentLog"/> to delete.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task DeleteById(string id);
    }
