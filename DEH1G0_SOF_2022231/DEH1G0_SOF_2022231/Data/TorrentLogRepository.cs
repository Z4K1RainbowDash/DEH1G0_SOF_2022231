using DEH1G0_SOF_2022231.Models;

namespace DEH1G0_SOF_2022231.Data;

/// <summary>
/// Repository class for TorrentLog entities
/// </summary>
public class TorrentLogRepository : GenericRepository<TorrentLog>, ITorrentLogRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TorrentLogRepository"/> class.
    /// </summary>
    /// <param name="context">The <see cref="ApplicationDbContext"/> to use for database operations.</param>
    public TorrentLogRepository(ApplicationDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Asynchronously deletes the <see cref="TorrentLog"/> with the given ID.
    /// </summary>
    /// <param name="id">The ID of the <see cref="TorrentLog"/> to delete.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public async Task DeleteById(string id)
    {
        try
        {
            var log = await this.GetByIdAsync(id);
            await this.DeleteAsync(log);
        }
        catch (Exception e)
        {
            throw new NullReferenceException("TorrentLog not found");
        }
    }
}

