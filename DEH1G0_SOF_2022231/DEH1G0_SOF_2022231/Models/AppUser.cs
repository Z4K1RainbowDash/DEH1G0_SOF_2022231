using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DEH1G0_SOF_2022231.Models
{
    public class AppUser : IdentityUser
    {

        [StringLength(200, MinimumLength = 2)]
        [Required]
        public string FirstName { get; set; }

        [StringLength(200, MinimumLength = 2)]
        [Required]
        public string LastName { get; set; }

        [StringLength(200)]
        public string? PhotoContentType { get; set; }

        public byte[]? PhotoData { get; set; }

        [NotMapped]
        public virtual ICollection<Torrent> Torrents { get; set; }

        public AppUser()
        {
            this.Torrents = new HashSet<Torrent>();
        }
    }
}
