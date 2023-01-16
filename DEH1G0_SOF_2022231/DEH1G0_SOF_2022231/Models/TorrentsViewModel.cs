using DEH1G0_SOF_2022231.Models.Helpers;
using System.ComponentModel.DataAnnotations;

namespace DEH1G0_SOF_2022231.Models
{
    /// <summary>
    /// The class represents the view model for displaying torrents
    /// </summary>
    public class TorrentsViewModel
    {
        /// <summary>
        /// The text used for searching torrents
        /// </summary>
        [Required]
        [StringLength(200, MinimumLength = 3)]
        public string SearchText { get; set; }

        /// <summary>
        /// A list of torrent categories
        /// </summary>
        public List<TorrentCategory> TorrentCategories { get; init; }
    }

}
