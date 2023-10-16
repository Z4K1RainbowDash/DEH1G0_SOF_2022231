namespace DEH1G0_SOF_2022231.Models.DTOs;

/// <summary>
/// Represents a DTO for a torrent and its users.
/// </summary>
public class TorrentUsersDto
{

    /// <summary>
    /// Gets or sets the torrent's name.
    /// </summary>
    public string TorrentName { get; init; }

    /// <summary>
    /// Gets or sets the collection of users who downloaded it.
    /// </summary>
    public IList<AppUser> AppUsers { get; init; }
}

