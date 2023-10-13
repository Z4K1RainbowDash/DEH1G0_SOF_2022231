using DEH1G0_SOF_2022231.Models.Helpers;
using System.Text;

namespace DEH1G0_SOF_2022231.Helpers;

    /// <summary>
    /// URL builder for Ncore.
    /// </summary>
    public class NcoreUrlBuilder : INcoreUrlBuilder
    {
        private const string _basicUrl = "/torrents.php?oldal=1&tipus=kivalasztottak_kozott&kivalasztott_tipus=";
        private StringBuilder _stringBuilder;

        private string _searchText = default!;
        private string _searchType = default!;

        /// <summary>
        /// Initializes a new instance of the <see cref="NcoreUrlBuilder"/> class.
        /// </summary>
        public NcoreUrlBuilder()
        {
            this._stringBuilder = new StringBuilder(_basicUrl);
            this._searchText = string.Empty;
            this._searchType = string.Empty;
        }

        /// <summary>
        /// Sets the properties to default values.
        /// </summary>
        /// <returns>The <see cref="NcoreUrlBuilder"/>.</returns>
        public NcoreUrlBuilder Reset()
        {
            this._stringBuilder = new StringBuilder(_basicUrl);
            this._searchText = string.Empty;
            this._searchType = string.Empty;

            return this;
        }

        /// <summary>
        /// Builds a NcoreUrl with options.
        /// </summary>
        /// <returns>A <see cref="NcoreUrl"/> object.</returns>
        public NcoreUrl Build()
        {
            // TODO: null or empty check
            if (this._stringBuilder[^1] == ',')
            {
                this._stringBuilder.Remove(this._stringBuilder.Length - 1, 1);
            }

            string returnString = $"{this._stringBuilder}&mire={this._searchText}&miben={this._searchType}";

            this._stringBuilder.Clear().Append(_basicUrl);
            return new NcoreUrl(returnString);
        }

        /// <summary>
        /// Sets the search text.
        /// </summary>
        /// <param name="searchText">This string will be the text to search for.</param>
        /// <returns>The <see cref="NcoreUrlBuilder"/>.</returns>
        public NcoreUrlBuilder SetSearchText(string searchText)
        {
            this._searchText = searchText.Replace(' ', '+');
            return this;
        }

        /// <summary>
        /// Sets the search type.
        /// </summary>
        /// <returns>The <see cref="NcoreUrlBuilder"/>.</returns>
        public NcoreUrlBuilder SetSearchType()
        {
            // TODO: more search type
            this._searchType = "name";

            return this;
        }

        /// <summary>
        /// Tests the input boolean variable and sets the input string to StringBuilder if the variable is true.
        /// </summary>
        /// <param name="condition">A boolean variable that is tested</param>
        /// <param name="text">A string that will be added to StringBuilder if the condition is true.</param>
        private void TestAndSet(bool condition, string text)
        {
            if (condition)
            {
                this._stringBuilder.Append(text);
            }
        }

        /// <summary>
        /// Sets movie categories based on the input Movies object.
        /// </summary>
        /// <param name="movies">An object of <see cref="Movies"/> class that contains the selected movie categories</param>
        /// <returns>The current <see cref="NcoreUrlBuilder"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the movies parameter is null</exception>
        public NcoreUrlBuilder SetMovieCategories(Movies movies)
        {
            if (movies == null)
            {
                throw new ArgumentNullException(nameof(movies));
            }
            if (movies.IsSelected)
            {
                this.TestAndSet(movies.SdHu, "xvid_hun,");
                this.TestAndSet(movies.SdEn, "xvid,");
                this.TestAndSet(movies.DvdrHu, "dvd_hun,");
                this.TestAndSet(movies.DvdrEn, "dvd,");
                this.TestAndSet(movies.Dvd9Hu, "dvd9_hun,");
                this.TestAndSet(movies.Dvd9En, "dvd9,");
                this.TestAndSet(movies.HdHu, "hd_hun,");
                this.TestAndSet(movies.HdEn, "hd,");
            }

            return this;
        }

        /// <summary>
        /// Sets music categories based on the input Music object.
        /// </summary>
        /// <param name="musics">An object of <see cref="Music"/> class that contains the selected music categories</param>
        /// <returns>The current <see cref="NcoreUrlBuilder"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the music parameter is null</exception>
        public NcoreUrlBuilder SetMusicCategories(Music musics)
        {
            if (musics == null)
            {
                throw new ArgumentNullException(nameof(musics));
            }
            if (musics.IsSelected)
            {
                this.TestAndSet(musics.Mp3Hu, "mp3_hun,");
                this.TestAndSet(musics.Mp3En, "mp3,");
                this.TestAndSet(musics.LosslessHu, "lossless_hun,");
                this.TestAndSet(musics.LosslessEn, "lossless,");
                this.TestAndSet(musics.Clip, "clip,");

            }

            return this;
        }


        /// <summary>
        /// Sets music categories based on the input Games object.
        /// </summary>
        /// <param name="games">An object of <see cref="Games"/> class that contains the selected game categories</param>
        /// <returns>The current <see cref="NcoreUrlBuilder"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the game parameter is null</exception>
        public NcoreUrlBuilder SetGameCategories(Games games)
        {
            if (games == null)
            {
                throw new ArgumentNullException(nameof(games));
            }
            if (games.IsSelected)
            {
                this.TestAndSet(games.Rip, "game_iso,");
                this.TestAndSet(games.Iso, "game_rip,");
                this.TestAndSet(games.Console, "console,");
                ;

            }

            return this;
        }

        /// <summary>
        /// Sets music categories based on the input Books object.
        /// </summary>
        /// <param name="books">An object of <see cref="Books"/> class that contains the selected book categories</param>
        /// <returns>The current <see cref="NcoreUrlBuilder"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the book parameter is null</exception>
        public NcoreUrlBuilder SetBookCategories(Books books)
        {
            if (books == null)
            {
                throw new ArgumentNullException(nameof(books));
            }
            if (books.IsSelected)
            {
                this.TestAndSet(books.EBookHu, "ebook_hun,");
                this.TestAndSet(books.EBookEn, "ebook,");
            }

            return this;
        }

        /// <summary>
        /// Sets series categories based on the input Series object.
        /// </summary>
        /// <param name="series">An object of <see cref="Series"/> class that contains the selected series categories</param>
        /// <returns>The current <see cref="NcoreUrlBuilder"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the series parameter is null</exception>
        public NcoreUrlBuilder SetSeriesCategories(Series series)
        {
            if (series == null)
            {
                throw new ArgumentNullException(nameof(series));
            }
            if (series.IsSelected)
            {
                this.TestAndSet(series.SdHu, "xvidser_hun,");
                this.TestAndSet(series.SdEn, "xvidser,");
                this.TestAndSet(series.DvdrHu, "dvdser_hun,");
                this.TestAndSet(series.DvdrEn, "dvdser,");
                this.TestAndSet(series.HdHu, "hdser_hun,");
                this.TestAndSet(series.HdEn, "hdser,");
            }

            return this;
        }


        /// <summary>
        /// Sets programs categories based on the input Programs object.
        /// </summary>
        /// <param name="programs">An object of <see cref="Programs"/> class that contains the selected programs categories</param>
        /// <returns>The current <see cref="NcoreUrlBuilder"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the programs parameter is null</exception>
        public NcoreUrlBuilder SetProgramCategories(Programs programs)
        {
            if (programs == null)
            {
                throw new ArgumentNullException(nameof(programs));
            }
            if (programs.IsSelected)
            {
                this.TestAndSet(programs.Iso, "iso,");
                this.TestAndSet(programs.Rip, "misc,");
                this.TestAndSet(programs.Mobile, "mobil,");
            }

            return this;
    }
}
