using DEH1G0_SOF_2022231.Data;
using DEH1G0_SOF_2022231.Models;
using DEH1G0_SOF_2022231.Models.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace DEH1G0_SOF_2022231.Controllers
{
    public class TorrentsController : Controller
    {


        public TorrentsController()
        {
          
        }
        public async Task<IActionResult> SearchTorrent()
        {
            TorrentsViewModel vm = new TorrentsViewModel();
            
            return View(vm);
            
        }

        [HttpPost]
        public async Task<IActionResult> SearchTorrent(TorrentsViewModel vm)
        {
            
            return View(vm);

        }


    }
}
