using DEH1G0_SOF_2022231.Data;
using DEH1G0_SOF_2022231.Helpers;
using DEH1G0_SOF_2022231.Models;
using DEH1G0_SOF_2022231.Models.Helpers;
using Microsoft.AspNetCore.SignalR;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace DEH1G0_SOF_2022231.Logic
{
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
        /// <param name="searchText">Text used for searching.</param>
        /// <param name="movies">An object of Movies class that contains the selected movie categories</param>
        /// <param name="series">An object of Series class that contains the selected series categories</param>
        /// <param name="musics">An object of Musics class that contains the selected music categories</param>
        /// <param name="programs">An object of Programs class that contains the selected program categories</param>
        /// <param name="games">An object of Games class that contains the selected game categories</param>
        /// <param name="books">An object of Books class that contains the selected book categories</param>
        /// <returns>The NcoreUrl object containing the built URL</returns>
        public NcoreUrl GetNcoreUrl(string searchText, Movies movies, Series series, Musics musics, Programs programs, Games games, Books books)
        {
            this._builder
                .Reset()
                .SetSearchText(searchText)
                .SetBookCategories(books)
                .SetGameCategories(games)
                .SetProgramCategories(programs)
                .SetMusicCategories(musics)
                .SetMovieCategories(movies)
                .SetSeriesCategories(series)
                .SetSearchType();

            return this._builder.Build();
        }

        /// <summary>
        /// This method creates identities for a specific torrent by searching for the torrent by id, creating a new torrent if it does not exist,
        /// linking the torrent to a user, and creating a new log entry for the torrent.
        /// </summary>
        /// <param name="torrentId">Id of the torrent</param>
        /// <param name="torrentName">Name of the torrent</param>
        /// <param name="userId">Id of the user</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task CreateIdentities(string torrentId, string torrentName, string userId)
        {
            Torrent torrent = await this._torrentRepository.GetByIdAsync(torrentId);

            if (torrent == null)
            {
                torrent = new Torrent
                {
                    NcoreId = torrentId,
                    Name = torrentName
                };
                await this._torrentRepository.AddAsync(torrent);
            }

            var user = await this._userRepository.GetByIdAsync(userId);

            torrent.AppUsers.Add(user);
            await this._torrentRepository.UpdateAsync(torrent);


            TorrentLog torrentLog = new TorrentLog()
            {
                Created = DateTime.Now,
                TorrentId = torrentId,
                UserId = userId
            };

            await this._torrentLogRepository.AddAsync(torrentLog);
        }

        


         
    }

}
