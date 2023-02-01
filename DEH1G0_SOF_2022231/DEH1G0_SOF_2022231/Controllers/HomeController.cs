using DEH1G0_SOF_2022231.Data;
using DEH1G0_SOF_2022231.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;

namespace DEH1G0_SOF_2022231.Controllers
{
    /// <summary>
    /// The HomeController is responsible for handling the main functions of the application.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<AppUser> _signManager;
        private readonly IAppUserRepository _userRepo;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="logger">The logger used for logging.</param>
        /// <param name="userRepository">The user repository used to interact with the user data.</param>
        /// <param name="userManager">The user manager used to manage user data.</param>
        /// <param name="roleManager">The role manager used to manage roles.</param>
        /// <param name="signInManager">The sign in manager used to manage sign in data.</param>
        public HomeController(ILogger<HomeController> logger, IAppUserRepository userRepository, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager)
        {
            _logger = logger;
            _userRepo = userRepository;
            _userManager = userManager;
            _roleManager = roleManager;
            _signManager = signInManager;
        }

        /// <summary>
        /// The Index action returns the Index view.
        /// </summary>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// The Privacy action returns the Privacy view.
        /// </summary>
        /// <returns>The Privacy view.</returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// The ListUsers action returns the view of all users.
        /// </summary>
        /// <returns>The view of all users.</returns>
        [Authorize]
        public async Task<IActionResult> ListUsers()
        {
            return View(await this._userRepo.GetAllAsync());
        }


        /// <summary>
        /// The DeleteUser action deletes the current user and logs the user out.
        /// </summary>
        /// <returns>The Index action.</returns>
        [Authorize]
        public async Task<IActionResult> DeleteUser()
        {
            var user = await _userManager.FindByIdAsync(_userManager.GetUserId(this.User));
            var roles = await _userManager.GetRolesAsync(user);
            
                    
            if (user != null)
            {
                await _signManager.SignOutAsync();
                
                await _userManager.DeleteAsync(user);
                
            
            }
            return RedirectToAction(nameof(Index));
        }
        /// <summary>
        /// Deletes the specified user account as an administrator.
        /// </summary>
        /// <param name="userid">The unique identifier of the user to delete.</param>
        /// <returns>A redirect to the action that lists all users in the system.</returns>
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUserByAdmin(string userid)
        {
            var user = await this._userRepo.GetByIdAsync(userid);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }

            return RedirectToAction(nameof(ListUsers));
        }

        /// <summary>
        /// Grants the role of "Admin" to the specified user.
        /// </summary>
        /// <param name="userid">The user ID of the user to grant the "Admin" role to.</param>
        /// <returns>The list of all users if the role is successfully granted, otherwise returns the default view.</returns>
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

        /// <summary>
        /// Remove the role of admin from a user.
        /// </summary>
        /// <param name="userid">The user's identifier.</param>
        /// <returns>Redirect to the `ListUsers` action.</returns>
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




        /// <summary>
        /// Gets the image for the user with the specified id.
        /// </summary>
        /// <param name="userid">The id of the user.</param>
        /// <returns>The image for the user with the specified id.</returns>
        public IActionResult GetImage(string userid)
        {
            var user = _userManager.Users.FirstOrDefault(t => t.Id == userid);
            return new FileContentResult(user.PhotoData, user.PhotoContentType);
        }

        /// <summary>
        /// Displays an error page.
        /// </summary>
        /// <returns>The error page view.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}