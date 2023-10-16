using DEH1G0_SOF_2022231.Models.Helpers.ModelBinders;
using Microsoft.AspNetCore.Mvc;

namespace DEH1G0_SOF_2022231.Models.DTOs;

/// <summary>
/// Represent a DTO for a selected torrent.
/// </summary>
[ModelBinder(BinderType = typeof(SelectedTorrentDtoBinder))]
public class SelectedTorrentDto
{
    /// <summary>
    /// Gets the torrent's id.
    /// </summary>
    public string TorrentId { get; init; }
    
    /// <summary>
    /// Gets the torrent's name.
    /// </summary>
    public string TorrentName { get; init; }
}