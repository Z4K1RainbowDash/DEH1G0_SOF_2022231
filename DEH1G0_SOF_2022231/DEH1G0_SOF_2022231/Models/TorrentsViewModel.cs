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

       

        public Movies Movies { get; set; }

        public Series Series { get; set; }

        public Musics Musics { get; set; }

        public Games Games { get; set; }
        public Programs Programs { get; set; }

        public Books Books { get; set; }

        public List<Torrent>  Torrents { get; set; }

    }

}
