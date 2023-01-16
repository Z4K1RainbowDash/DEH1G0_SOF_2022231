using Microsoft.AspNetCore.Mvc;

namespace DEH1G0_SOF_2022231.Controllers
{
    public class TorrentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
