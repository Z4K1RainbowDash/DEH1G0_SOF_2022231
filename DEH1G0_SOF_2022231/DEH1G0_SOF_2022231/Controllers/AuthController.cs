using DEH1G0_SOF_2022231.Models;
using DEH1G0_SOF_2022231.Models.Auth;
using DEH1G0_SOF_2022231.Models.Helpers.ModelBinders;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DEH1G0_SOF_2022231.Controllers
{

    /// <summary>
    /// This controller handles the authorisation functions.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="userManager">The user manager used to manage user data.</param>
        /// <exception cref="ArgumentNullException"> Thrown if the provided UserManager instance is null.</exception>
        public AuthController(UserManager<AppUser> userManager)
        {
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
            
            
            // await _userManager.AddToRoleAsync(user, "Role");
            var result = await _userManager.CreateAsync(user, registerModel.Password);
            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return Problem("Duplicated User");
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
                return BadRequest(ModelState);
            }
            
            var user = await _userManager.FindByNameAsync(loginModel.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
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
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }
    }
}
