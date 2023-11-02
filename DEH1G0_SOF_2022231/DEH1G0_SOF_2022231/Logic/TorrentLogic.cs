using DEH1G0_SOF_2022231.Data;
using DEH1G0_SOF_2022231.Helpers;
using DEH1G0_SOF_2022231.Models;
using DEH1G0_SOF_2022231.Models.DTOs;

namespace DEH1G0_SOF_2022231.Logic;

/// <summary>
/// Contains the logic for building Ncore Url
/// </summary>
public class TorrentLogic : ITorrentLogic
{
    private readonly INcoreUrlBuilder _builder;
    private readonly IAppUserRepository _userRepository;
    private readonly ITorrentRepository _torrentRepository;
    private readonly ITorrentLogRepository _torrentLogRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="TorrentLogic"/> class.
    /// </summary>
    /// <param name="builder">An object of INcoreUrlBuilder that is used for building the Ncore Url</param>
    /// <exception cref="ArgumentNullException">Thrown if the builder parameter is null</exception>
    public TorrentLogic(INcoreUrlBuilder builder, ITorrentRepository torrentRepository, IAppUserRepository appUserRepository, ITorrentLogRepository torrentLogRepository)
    {
        _builder = builder ?? throw new ArgumentNullException(nameof(builder));
        _torrentRepository = torrentRepository;
        _userRepository = appUserRepository;
        _torrentLogRepository = torrentLogRepository;
    }

    /// <summary>
    /// Builds an Ncore url based on the input search text and categories.
    /// </summary>
    /// <param name="dto">DTO that contains the selected torrent categories and the search text.</param>
    /// <returns>The NcoreUrl object containing the built URL.</returns>
    public NcoreUrl GetNcoreUrl(TorrentSearchDto dto)
    {
        this._builder
            .Reset()
            .SetSearchText(dto.SearchText)
            .SetBookCategories(dto.Books)
            .SetGameCategories(dto.Games)
            .SetProgramCategories(dto.Programs)
            .SetMusicCategories(dto.Music)
            .SetMovieCategories(dto.Movies)
            .SetSeriesCategories(dto.Series)
            .SetSearchType();

        return this._builder.Build();
    }

    /// <summary>
    /// This method creates identities for a specific torrent by searching for the torrent by id, creating a new torrent if it does not exist,
    /// linking the torrent to a user, and creating a new log entry for the torrent.
    /// </summary>
    /// <param name="dto">DTO that contains the selected torrent data.</param>
    /// <param name="userId">Id of the user</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task CreateIdentities(SelectedTorrentDto dto, AppUser user)
    {

        var torrent = await this.GetOrCreateTorrent(dto);

        await this.AddUserToTorrent(torrent, user);

        TorrentLog torrentLog = new TorrentLog()
        {
            Created = DateTime.Now,
            TorrentId = dto.TorrentId,
            UserId = user.Id
        };

        await this._torrentLogRepository.AddAsync(torrentLog);


    }

    /// <summary>
    /// Retrieves a list of Torrents, sorted in descending order based on the number of associated AppUsers.
    /// </summary>
    /// <returns>A list of Torrents sorted by their number of associated AppUsers.</returns>
    /// <exception cref="Exception">Throws an exception if there's an error while fetching or sorting the torrents.</exception>
    public async Task<IList<Torrent>> MostActiveTorrentsByDownloadsAsync()
    {
        try
        {
            var torrents = await this._torrentRepository.GetAllAsync();
            var sortedTorrents = torrents.OrderByDescending(u => u.AppUsers.Count).ToList();

            return sortedTorrents;
        }
        catch (Exception e)
        {
            throw new Exception("Error fetching or sorting torrents.", e);
        }
    }

    private async Task<Torrent> GetOrCreateTorrent(SelectedTorrentDto dto)
    {
        Torrent? torrent = await this._torrentRepository.GetByIdAsync(dto.TorrentId);

        if (torrent == null)
        {
            torrent = new Torrent
            {
                NcoreId = dto.TorrentId,
                Name = dto.TorrentName.Replace('_', ' ')
            };

            await this._torrentRepository.AddAsync(torrent);
        }

        return torrent;
    }

    private async Task AddUserToTorrent(Torrent torrent, AppUser user)
    {
        torrent.AppUsers.Add(user);
        await this._torrentRepository.UpdateAsync(torrent);
    }
}