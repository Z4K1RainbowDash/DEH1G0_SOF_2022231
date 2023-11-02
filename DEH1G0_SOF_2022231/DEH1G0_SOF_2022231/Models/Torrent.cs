using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DEH1G0_SOF_2022231.Models;

/// <summary>
/// This class represents a torrent.
/// </summary>
public class Torrent
{
    /// <summary>
    /// Gets or sets the Ncore ID of the torrent.
    /// </summary>
    [Key]
    public string NcoreId { get; set; }

    /// <summary>
    /// Gets or sets the name of the torrent.
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the collection of users who downloaded it.
    /// </summary>
    [NotMapped]
    public virtual ICollection<AppUser> AppUsers { get; set; }

    /// <summary>
    ///  Initializes a new instance of the <see cref="Torrent"/> class.
    /// </summary>
    public Torrent()
    {
        this.AppUsers = new HashSet<AppUser>();
    }
}

