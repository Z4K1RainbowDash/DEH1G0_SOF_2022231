using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiClient
{
    public class TorrentLog
    {
        public string Id { get; set; }
        public DateTime Created { get; set; }
        public string TorrentId { get; set; }
        public string UserId { get; set; }
    }
}
