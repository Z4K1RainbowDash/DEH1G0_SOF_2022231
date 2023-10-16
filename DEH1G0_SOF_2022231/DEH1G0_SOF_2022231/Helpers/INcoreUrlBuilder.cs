using DEH1G0_SOF_2022231.Models.Helpers;

namespace DEH1G0_SOF_2022231.Helpers;
/// <summary>
/// Represent an URL builder interface for Ncore.
/// </summary>
public interface INcoreUrlBuilder
{
    
    /// <summary>
    /// Builds a NcoreUrl with options.
    /// </summary>
    /// <returns>A <see cref="NcoreUrl"/> object.</returns>
    NcoreUrl Build();
    
    /// <summary>
    /// Sets the properties to default values.
    /// </summary>
    /// <returns>The <see cref="NcoreUrlBuilder"/>.</returns>
    NcoreUrlBuilder Reset();
    
    /// <summary>
    /// Sets music categories based on the input Books object.
    /// </summary>
    /// <param name="books">An object of <see cref="Books"/> class that contains the selected book categories</param>
    /// <returns>The current <see cref="NcoreUrlBuilder"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the book parameter is null</exception>
    NcoreUrlBuilder SetBookCategories(Books books);
    
    /// <summary>
    /// Sets music categories based on the input Games object.
    /// </summary>
    /// <param name="games">An object of <see cref="Games"/> class that contains the selected game categories</param>
    /// <returns>The current <see cref="NcoreUrlBuilder"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the game parameter is null</exception>
    NcoreUrlBuilder SetGameCategories(Games games);
    
    /// <summary>
    /// Sets movie categories based on the input Movies object.
    /// </summary>
    /// <param name="movies">An object of <see cref="Movies"/> class that contains the selected movie categories</param>
    /// <returns>The current <see cref="NcoreUrlBuilder"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the movies parameter is null</exception>
    NcoreUrlBuilder SetMovieCategories(Movies movies);
    
    /// <summary>
    /// Sets music categories based on the input Music object.
    /// </summary>
    /// <param name="musics">An object of <see cref="Music"/> class that contains the selected music categories</param>
    /// <returns>The current <see cref="NcoreUrlBuilder"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the music parameter is null</exception>
    NcoreUrlBuilder SetMusicCategories(Music musics);
    
    /// <summary>
    /// Sets programs categories based on the input Programs object.
    /// </summary>
    /// <param name="programs">An object of <see cref="Programs"/> class that contains the selected programs categories</param>
    /// <returns>The current <see cref="NcoreUrlBuilder"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the programs parameter is null</exception>
    NcoreUrlBuilder SetProgramCategories(Programs programs);
    
    /// <summary>
    /// Sets the search text.
    /// </summary>
    /// <param name="searchText">This string will be the text to search for.</param>
    /// <returns>The <see cref="NcoreUrlBuilder"/>.</returns>
    NcoreUrlBuilder SetSearchText(string searchText);
    
    /// <summary>
    /// Sets the search type.
    /// </summary>
    /// <returns>The <see cref="NcoreUrlBuilder"/>.</returns>
    NcoreUrlBuilder SetSearchType();
    
    /// <summary>
    /// Sets series categories based on the input Series object.
    /// </summary>
    /// <param name="series">An object of <see cref="Series"/> class that contains the selected series categories</param>
    /// <returns>The current <see cref="NcoreUrlBuilder"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the series parameter is null</exception>
    NcoreUrlBuilder SetSeriesCategories(Series series);
}