using DEH1G0_SOF_2022231.Data;
using DEH1G0_SOF_2022231.Helpers;
using DEH1G0_SOF_2022231.Models;
using DEH1G0_SOF_2022231.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DEH1G0_SOF_2022231.Controllers;

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

    /// <summary>
    /// Initializes a new instance of the <see cref="HomeController"/> class.
    /// </summary>
    /// <param name="logger">The logger used for logging.</param>
    /// <param name="userRepository">The user repository used to interact with the user data.</param>
    /// <param name="userManager">The user manager used to manage user data.</param>
    /// <param name="roleManager">The role manager used to manage roles.</param>
    /// <param name="signInManager">The sign in manager used to manage sign in data.</param>
    public HomeController(ILogger<HomeController> logger, IAppUserRepository userRepository,
        UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _userRepo = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        _signManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
    }


    /// <summary>
    /// Get limited users.
    /// </summary>
    /// <returns>
    /// <see cref="OkResult"/> with users.
    /// <see cref="StatusCodeResult"/> with a status code of 500 (Internal Server Error) if an error occurs while getting the users.
    /// </returns>
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BasicUserInfosDto>>> ListUsersAsync(
        [FromQuery] PageQueryParameters pageQueryParameters)
    {
        try
        {
            var paginatedAppUsersAsync = await this._userRepo.GetPaginatedAppUsersAsync(pageQueryParameters);
            var metadata = new
            {
                paginatedAppUsersAsync.PageIndex,
                paginatedAppUsersAsync.TotalPages,
                paginatedAppUsersAsync.HasNextPage,

                paginatedAppUsersAsync.HasPreviousPage,
                pageQueryParameters.PageSize

            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            var userDtOs = new List<BasicUserInfosDto>();

            foreach (var user in paginatedAppUsersAsync)
            {
                var userDto = new BasicUserInfosDto()
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

                userDtOs.Add(userDto);
            }

            this._logger.LogInformation("Fetched {NumberOfUsers} users. Pagination details: {PaginationDetails}",
                userDtOs.Count, metadata);
            return Ok(userDtOs);
        }
        catch (Exception ex)
        {
            this._logger.LogError(ex, "Error occurred while getting users in {MethodName}", nameof(ListUsersAsync));
            return StatusCode(StatusCodes.Status500InternalServerError);
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
    /// <response code="200">if the user is successfully deleted.</response>
    /// <response code="400">if the user is not found with the provided user ID.</response>
    /// <response code="500">if an error occurs while deleting the user.</response>
    [Authorize]
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteUserAsync()
    {
        var user = await this._userManager.FindByIdAsync(this._userManager.GetUserId(this.User));

        if (user != null)
        {
            try
            {
                await this._signManager.SignOutAsync();
                await this._userManager.DeleteAsync(user);
                this._logger.LogInformation("User {UserName} deleted in successfully", user.UserName);

                return Ok();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Error occurred while deleting user");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        this._logger.LogError("User not found in {MethodName}", nameof(DeleteUserAsync));
        return BadRequest();
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
    /// <response code="200">if the user is successfully deleted.</response>
    /// <response code="400">if the user is not found with the provided user ID.</response>
    /// <response code="500">if an error occurs while deleting the user.</response>
    [Authorize(Roles = "Admin")]
    [HttpDelete]
    public async Task<IActionResult> DeleteUserByAdminAsync(string userid)
    {
        var user = await this._userManager.FindByIdAsync(userid);
        string adminUsername = this.User?.Identity?.Name!;

        if (user != null)
        {
            try
            {
                await this._userManager.DeleteAsync(user);
                this._logger.LogInformation("Admin {AdminUsername} deleted user {UserId} successfully", adminUsername,
                    user.Id);
                return Ok();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Error occurred while deleting user (ID:{UserId}) by {AdminUsername}", userid,
                    adminUsername);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        this._logger.LogWarning("Admin {AdminUsername} attempted to delete a non-existent user", adminUsername);
        return BadRequest(this._userNotFoundErrorMessage);
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
    /// <response code="200">if the "Admin" role is successfully added to the user.</response>
    /// <response code="400">if the user is not found with the provided user ID.</response>
    /// <response code="500">if an error occurs while adding the role.</response>
    [Authorize(Roles = "Admin")]
    [HttpPut]
    public async Task<IActionResult> GrantAdminAsync(string userid)
    {
        var user = await this._userManager.FindByIdAsync(userid);
        var adminUsername = this.User?.Identity?.Name;
        if (user != null)
        {
            try
            {
                await this._userManager.AddToRoleAsync(user, "Admin");
                this._logger.LogInformation("Admin {AdminUsername} successfully granted Admin role for {Username}",
                    adminUsername, user.UserName);
                return Ok();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Error occurred while granting Admin role for user (ID: {UserId})", userid);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        this._logger.LogWarning("Admin {AdminUsername} attempted to grant admin role to a non-existent user",
            adminUsername);
        return BadRequest(this._userNotFoundErrorMessage);
    }

    /// <summary>
    /// Remove the role of admin from a user.
    /// </summary>
    /// <param name="userid">The user's identifier.</param>
    /// <returns>
    /// <see cref="OkResult"/> if the "Admin" role is successfully removed from the user.
    /// <see cref="BadRequestResult"/> if the user is not found with the provided user ID.
    /// <see cref="StatusCodeResult"/> if an error occurs while removing the role.
    /// </returns>
    /// <response code="200">if the "Admin" role is successfully removed from the user.</response>
    /// <response code="400">if the user is not found with the provided user ID.</response>
    /// <response code="500">if an error occurs while removing the role.</response>
    [Authorize(Roles = "Admin")]
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RemoveAdminAsync(string userid)
    {
        var user = await this._userManager.FindByIdAsync(userid);
        // Do i need 
        var adminUsername = this.User?.Identity?.Name;
        if (user != null)
        {
            try
            {
                await this._userManager.RemoveFromRoleAsync(user, "Admin");
                this._logger.LogInformation("Admin {AdminUsername} removed admin role from user(ID: {UserId})",
                    adminUsername, userid);
                return Ok();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex,"Error occurred while removing Admin role from user(ID: {UserId}) by {AdminName}",
                    userid, adminUsername);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        this._logger.LogWarning("Admin {AdminUsername} attempted to remove admin role from a non-existent user",
            adminUsername);
        return BadRequest(this._userNotFoundErrorMessage);
    }
}