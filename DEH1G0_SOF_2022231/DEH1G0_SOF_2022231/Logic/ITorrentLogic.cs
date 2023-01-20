using DEH1G0_SOF_2022231.Helpers;
using DEH1G0_SOF_2022231.Models.Helpers;

namespace DEH1G0_SOF_2022231.Logic
{
    public interface ITorrentLogic
    {
        NcoreUrl GetNcoreUrl(string searchText, Movies movies, Series series, Musics musics, Programs programs, Games games, Books books);
    }
}