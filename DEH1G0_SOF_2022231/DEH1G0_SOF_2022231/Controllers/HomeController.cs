using DEH1G0_SOF_2022231.Data;
using DEH1G0_SOF_2022231.Models;
using DEH1G0_SOF_2022231.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DEH1G0_SOF_2022231.Controllers
{
    /// <summary>
    /// The HomeController is responsible for handling the main functions of the application.
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class HomeController : ControllerBase
    {
        // dependencies
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<AppUser> _signManager;
        private readonly IAppUserRepository _userRepo;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        // return messages
        private readonly string _userNotFoundErrorMessage = "User not found with the provided user ID";
        private readonly string _errorOccurredMessage = "An error occurred while";



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
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userRepo = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _signManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        }


        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns>
        /// <see cref="OkResult"/> with users.
        /// <see cref="StatusCodeResult"/> with a status code of 500 (Internal Server Error) if an error occurs while getting the users.
        /// </returns>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BasicUserInfosDTO>>> ListUsersAsync()
        {
            try
            {
                var users = await this._userRepo.GetAllAsync();
                var userDTOs = new List<BasicUserInfosDTO>();

                foreach (var user in users)
                {
                    var userDTO = new BasicUserInfosDTO()
                    {
                        Id = user.Id,
                        Username = user.UserName,
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        EmailConfirmed = user.EmailConfirmed,
                        AccessFailedCount = user.AccessFailedCount,
                        Roles = await this._userManager.GetRolesAsync(user)
                    };
            
                    userDTOs.Add(userDTO);
                }

                return Ok(userDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{this._errorOccurredMessage} retrieving the list of users: {ex.Message}");
            }
        }

        /// <summary>
        /// This action deletes the current user and logs the user out.
        /// </summary>
        /// <returns>
        /// <see cref="OkResult"/> if the user is successfully deleted.
        /// <see cref="BadRequestResult"/> if the user is not found with the provided user ID.
        /// <see cref="StatusCodeResult"/> with a status code of 500 (Internal Server Error) if an error occurs while deleting the user.
        /// </returns>
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteUserAsync()
        {
            var user = await this._userManager.FindByIdAsync(this._userManager.GetUserId(this.User));

            if (user != null)
            {
                try
                {
                    await this._signManager.SignOutAsync();
                    await this._userManager.DeleteAsync(user);

                    return Ok();
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, $"{this._errorOccurredMessage} deleting the user: {ex.Message}");
                }
            }
            else
            {
                return BadRequest(this._userNotFoundErrorMessage);
            }
        }

        /// <summary>
        /// Deletes the specified user account as an administrator.
        /// </summary>
        /// <param name="userid">The user's identifier.</param>
        /// <returns>
        /// <see cref="OkResult"/> if the user is successfully deleted.
        /// <see cref="BadRequestResult"/> if the user is not found with the provided user ID.
        /// <see cref="StatusCodeResult"/> with a status code of 500 (Internal Server Error) if an error occurs while deleting the user.
        /// </returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUserByAdminAsync(string userid)
        {
            var user = await this._userManager.FindByIdAsync(userid);
            if (user != null)
            {
                try
                {
                    await this._userManager.DeleteAsync(user);
                    return Ok();
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, $"{this._errorOccurredMessage} delete the user: {ex.Message}");
                }
            }
            else
            {
                return BadRequest(this._userNotFoundErrorMessage);
            }
        }

        /// <summary>
        /// Grants the role of "Admin" to the specified user.
        /// </summary>
        /// <param name="userid">The user's identifier.</param>
        /// <returns>
        /// <see cref="OkResult"/> if the "Admin" role is successfully added to the user.
        /// <see cref="BadRequestResult"/> if the user is not found with the provided user ID.
        /// <see cref="StatusCodeResult"/> with a status code of 500 (Internal Server Error) if an error occurs while adding the role.
        /// </returns>
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> GrantAdminAsync(string userid)
        {
            var user = await this._userManager.FindByIdAsync(userid);
            if (user != null)
            {
                try
                {
                    await this._userManager.AddToRoleAsync(user, "Admin");
                    return Ok();
                }
                catch (Exception ex)
                {

                    return StatusCode(StatusCodes.Status500InternalServerError, $"{this._errorOccurredMessage} adding the role: {ex.Message}");
                }
            }
            return BadRequest(this._userNotFoundErrorMessage);
        }

        /// <summary>
        /// Remove the role of admin from a user.
        /// </summary>
        /// <param name="userid">The user's identifier.</param>
        /// <returns>
        /// <see cref="OkResult"/> if the "Admin" role is successfully removed from the user.
        /// <see cref="BadRequestResult"/> if the user is not found with the provided user ID.
        /// <see cref="StatusCodeResult"/> with a status code of 500 (Internal Server Error) if an error occurs while removing the role.
        /// </returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> RemoveAdminAsync(string userid)
        {
            var user = await this._userManager.FindByIdAsync(userid);
            if (user != null)
            {
                try
                {
                    await this._userManager.RemoveFromRoleAsync(user, "Admin");
                    return Ok();
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, $"{this._errorOccurredMessage} removing the role: {ex.Message}");
                }
            }
            return BadRequest(this._userNotFoundErrorMessage);
        }
    }
}