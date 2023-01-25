using DEH1G0_SOF_2022231.Data;
using DEH1G0_SOF_2022231.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;

namespace DEH1G0_SOF_2022231.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAppUserRepository _userRepo;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public HomeController(ILogger<HomeController> logger, IAppUserRepository userRepository, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _userRepo = userRepository;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [Authorize]
        public async Task<IActionResult> ListUsers()
        {
            return View(await this._userRepo.GetAllAsync());
        }


        [Authorize]
        public async Task<IActionResult> DeleteUser()
        {
            var user = await this._userRepo.GetByIdAsync(_userManager.GetUserId(this.User));
            if (user != null)
            {
                await this._userRepo.DeleteAsync(user);
            }

            return RedirectToAction(nameof(ListUsers));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUserByAdmin(string userid)
        {
            var user = await this._userRepo.GetByIdAsync(userid);
            if (user != null)
            {
                await this._userRepo.DeleteAsync(user);
            }

            return RedirectToAction(nameof(ListUsers));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GrantAdmin(string userid)
        {
            var user = this._userManager.Users.FirstOrDefault(t => t.Id == userid);
            if (user != null)
            {
                await _userManager.AddToRoleAsync(user, "Admin");
            }
            return RedirectToAction(nameof(ListUsers));
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveAdmin(string userid)
        {
            var user = _userManager.Users.FirstOrDefault(t => t.Id == userid);
            if (user != null)
            {
                await _userManager.RemoveFromRoleAsync(user, "Admin");
            }
            return RedirectToAction(nameof(ListUsers));

        }





        public IActionResult GetImage(string userid)
        {
            var user = _userManager.Users.FirstOrDefault(t => t.Id == userid);
            return new FileContentResult(user.PhotoData, user.PhotoContentType);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}