using System.ComponentModel.DataAnnotations;
using DEH1G0_SOF_2022231.Models.Helpers;
using DEH1G0_SOF_2022231.Models.Helpers.ModelBinders;
using Microsoft.AspNetCore.Mvc;

namespace DEH1G0_SOF_2022231.Models.DTOs;

/// <summary>
/// Represent a DTO for searching torrents.
/// </summary>
[ModelBinder(BinderType = typeof(TorrentSearchDtoBinder))]
public class TorrentSearchDto
{
    /// <summary>
    /// The text used for searching torrents
    /// </summary>
    [Required]
    [StringLength(200, MinimumLength = 3)]
    public string SearchText { get; init; }

    /// <summary>
    /// Gets or sets the torrent movies subcategories.
    /// </summary>
    public Movies Movies { get; init; }

    /// <summary>
    /// Gets or sets the torrent series subcategories.
    /// </summary>
    public Series Series { get; init; }

    /// <summary>
    /// Gets or sets the torrent music subcategories.
    /// </summary>
    public Music Music { get; init; }

    /// <summary>
    /// Gets or sets the torrent games subcategories.
    /// </summary>
    public Games Games { get; init; }

    /// <summary>
    /// Gets or sets the torrent Programs subcategories.
    /// </summary>
    public Programs Programs { get; init; }

    /// <summary>
    /// Gets or sets the torrent Books subcategories.
    /// </summary>
    public Books Books { get; init; }
}
