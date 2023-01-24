using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DEH1G0_SOF_2022231.Models
{
    public class TorrentLog
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public DateTime Created { get; set; }

        [ForeignKey("Torrent")]
        public string TorrentId { get; set; }

        [NotMapped]
        public virtual Torrent Torrent { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        [NotMapped]
        public virtual AppUser User { get; set; }

        public TorrentLog()
        {
                this.Id = Guid.NewGuid().ToString();
        }
    }
}
