using DEH1G0_SOF_2022231.Helpers;
using DEH1G0_SOF_2022231.Models;
using DEH1G0_SOF_2022231.Models.Helpers;

namespace DEH1G0_SOF_2022231.Logic
{
    /// <summary>
    /// Contains the logic for building Ncore Url
    /// </summary>
    public class TorrentLogic : ITorrentLogic
    {
        private readonly INcoreUrlBuilder _builder;

        /// <summary>
        /// Initializes a new instance of the <see cref="TorrentLogic"/> class.
        /// </summary>
        /// <param name="builder">An object of INcoreUrlBuilder that is used for building the Ncore Url</param>
        /// <exception cref="ArgumentNullException">Thrown if the builder parameter is null</exception>
        public TorrentLogic(INcoreUrlBuilder builder)
        {
            _builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }

        /// <summary>
        /// Returns the NcoreUrl based on the input search text and categories.
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
    }

}
