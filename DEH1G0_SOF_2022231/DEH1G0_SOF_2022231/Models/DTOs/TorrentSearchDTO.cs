using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DEH1G0_SOF_2022231.Models.Helpers;
using DEH1G0_SOF_2022231.Models.Helpers.ModelBinders;
using Microsoft.AspNetCore.Mvc;
using NcoreGrpcService.Protos;

namespace DEH1G0_SOF_2022231.Models.DTOs;

    /// <summary>
    /// The class represents the view model for displaying torrents
    /// </summary>
    [ModelBinder(BinderType = typeof(TorrentSearchDTOBinder))]
    public class TorrentSearchDTO
    {
        /// <summary>
        /// The text used for searching torrents
        /// </summary>
        [Required]
        [StringLength(200, MinimumLength = 3)]
        [DisplayName("Search Field:")]
        public string SearchText { get; set; }
        public Movies Movies { get; set; }

        public Series Series { get; set; }

        public Music Music { get; set; }

        public Games Games { get; set; }
        public Programs Programs { get; set; }

        public Books Books { get; set; }

    /// <summary>

}
