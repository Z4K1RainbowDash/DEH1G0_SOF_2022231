using DEH1G0_SOF_2022231.Models;
using DEH1G0_SOF_2022231.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DEH1G0_SOF_2022231.Controllers;

/// <summary>
/// This controller handles the authorisation functions.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ILogger<AuthController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthController"/> class.
    /// </summary>
    /// <param name="userManager">The user manager used to manage user data.</param>
    /// <exception cref="ArgumentNullException"> Thrown if the provided UserManager instance is null.</exception>
    public AuthController(UserManager<AppUser> userManager, ILogger<AuthController> logger)
    {
        this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    }

    /// <summary>
    /// Handles the user registration process.
    /// </summary>
    /// <param name="registerModel">The <see cref="RegisterModel"/> containing user registration information.</param>
    /// <returns>
    /// It returns an HTTP 200 OK response if the registration data is valid and the registration is successful, otherwise it returns an HTTP 400 Bad Request response with the validation errors.
    /// </returns>
    [HttpPut]
    public async Task<IActionResult> Register(RegisterModel registerModel)
    {
        if (!ModelState.IsValid)
        {
            this._logger.LogError("Invalid registration model");
            return BadRequest(ModelState);
        }

        var user = new AppUser
        {
            Email = registerModel.Email,
            UserName = registerModel.Username,
            LastName = registerModel.LastName,
            FirstName = registerModel.FirstName,
            SecurityStamp = Guid.NewGuid().ToString(),
        };

        try
        {
            // await _userManager.AddToRoleAsync(user, "Role");
            var result = await _userManager.CreateAsync(user, registerModel.Password);
            if (result.Succeeded)
            {
                this._logger.LogInformation("User registration succeeded for {Username}", user.UserName);
                return Ok();
            }
            this._logger.LogWarning("User registration failed for {Username}, user already exists", user.UserName);
            return Problem("Duplicated User");
        }
        catch (Exception e)
        {
            this._logger.LogError(e, "An error occurred while registering {Username}", user.UserName);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Handles the user login process.
    /// </summary>
    /// <param name="loginModel">The <see cref="LoginModel"/> containing user login credentials.</param>
    /// <returns>
    /// It returns an HTTP 200 OK response with a JWT token If the login credentials are valid, otherwise it return an HTTP 401 Unauthorized response.
    /// </returns>
    [HttpPost]
    public async Task<IActionResult> Login(LoginModel loginModel)
    {
        if (!ModelState.IsValid)
        {
            this._logger.LogWarning("Login attempt with invalid model state");
            return BadRequest(ModelState);
        }

        var user = await _userManager.FindByNameAsync(loginModel.UserName);
        if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
        {
            try
            {
                var claim = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                    new Claim(JwtRegisteredClaimNames.NameId, user.Id)
                };
                foreach (var role in await _userManager.GetRolesAsync(user))
                {
                    claim.Add(new Claim(ClaimTypes.Role, role));
                }
                var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("verylongsecretkey"));
                
                var token = new JwtSecurityToken(
                issuer: "http://www.security.org", audience: "http://www.security.org",
                claims: claim, expires: DateTime.Now.AddMinutes(60),
                signingCredentials: new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256)
                );
                
                this._logger.LogInformation("User {UserName} logged in successfully",loginModel.UserName);
                return Ok(
                    new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    });
            }
            catch (Exception e)
            {
                this._logger.LogError(e, "Error occurred while generating JWT token for user {UserName}", loginModel.UserName);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        this._logger.LogWarning("Unauthorized login attempt for user {UserName}",loginModel.UserName);
        return Unauthorized();
    }
}

