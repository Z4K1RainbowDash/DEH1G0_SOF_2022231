using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DEH1G0_SOF_2022231.Models;

/// <summary>
/// Represent an application user with extended properties.
/// </summary>
public class AppUser : IdentityUser
{

    /// <summary>
    /// Gets or sets the user's first name.
    /// </summary>
    [StringLength(200, MinimumLength = 2)]
    [Required]
    public string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the user's last name.
    /// </summary>
    [StringLength(200, MinimumLength = 2)]
    [Required]
    public string LastName { get; set; }

    /// <summary>
    /// Gets or sets the user's photo content type.
    /// </summary>
    [StringLength(200)]
    public string? PhotoContentType { get; set; }

    /// <summary>
    /// Gets or sets the user's photo data.
    /// </summary>
    public byte[]? PhotoData { get; set; }

    /// <summary>
    /// Gets or sets the collection of torrents downloaded by the user.
    /// </summary>
    [NotMapped]
    public virtual ICollection<Torrent> Torrents { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AppUser"/> class.
    /// </summary>
    public AppUser()
    {
        this.Torrents = new HashSet<Torrent>();
    }
}
