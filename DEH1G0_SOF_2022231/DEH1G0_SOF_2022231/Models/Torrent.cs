using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DEH1G0_SOF_2022231.Models
{
    public class Torrent
    {
        [Key]
        public string NcoreId { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public string CreatedDateTime { get; set; }

        [Required]
        public string Size { get; set; } = string.Empty;

        [Required]
        public string Downloads { get; set; }

        [Required]
        public int Seeders { get; set; }

        [Required]
        public int Leechers { get; set; }

        [NotMapped]
        public virtual ICollection<AppUser> AppUsers { get; set; }

        public Torrent()
        {
            this.AppUsers = new HashSet<AppUser>();
        }

    }
}
