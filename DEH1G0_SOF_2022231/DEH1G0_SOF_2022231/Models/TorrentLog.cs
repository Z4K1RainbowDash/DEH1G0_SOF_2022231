using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DEH1G0_SOF_2022231.Models;

/// <summary>
/// This class represents a log entry for a torrent.
/// </summary>
public class TorrentLog
{

    /// <summary>
    /// Gets or sets the ID of the log entry.
    /// </summary>
    [Key]
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the log entry was created.
    /// </summary>
    [Required]
    public DateTime Created { get; set; }

    /// <summary>
    /// Gets or sets the ID of the torrent associated with the log entry.
    /// </summary>
    [ForeignKey("Torrent")]
    public string TorrentId { get; set; }

    /// <summary>
    /// Gets or sets the torrent associated with the log entry.
    /// </summary>
    [NotMapped]
    [JsonIgnore]
    public virtual Torrent Torrent { get; set; }

    /// <summary>
    /// Gets or sets the ID of the user associated with the log entry.
    /// </summary>
    [ForeignKey("User")]
    public string UserId { get; set; }

    /// <summary>
    /// Gets or sets the user associated with the log entry.
    /// </summary>
    [NotMapped]
    [JsonIgnore]
    public virtual AppUser User { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="TorrentLog"/> class.
    /// </summary>
    public TorrentLog()
    {
        this.Id = Guid.NewGuid().ToString();
    }
}
