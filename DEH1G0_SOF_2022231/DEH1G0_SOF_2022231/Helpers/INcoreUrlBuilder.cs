using DEH1G0_SOF_2022231.Models.Helpers;

namespace DEH1G0_SOF_2022231.Helpers
{
    public interface INcoreUrlBuilder
    {
        NcoreUrl Build();
        NcoreUrlBuilder Reset();
        NcoreUrlBuilder SetBookCategories(Books books);
        NcoreUrlBuilder SetGameCategories(Games games);
        NcoreUrlBuilder SetMovieCategories(Movies movies);
        NcoreUrlBuilder SetMusicCategories(Musics musics);
        NcoreUrlBuilder SetProgramCategories(Programs programs);
        NcoreUrlBuilder SetSearchText(string searchText);
        NcoreUrlBuilder SetSearchType();
        NcoreUrlBuilder SetSeriesCategories(Series series);
    }
}