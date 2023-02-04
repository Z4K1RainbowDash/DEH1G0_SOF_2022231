// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using DEH1G0_SOF_2022231.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace DEH1G0_SOF_2022231.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ExternalLoginModel : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserStore<AppUser> _userStore;
        private readonly IUserEmailStore<AppUser> _emailStore;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<ExternalLoginModel> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _config;

        public ExternalLoginModel(
            SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            IUserStore<AppUser> userStore,
            ILogger<ExternalLoginModel> logger,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager,
            IConfiguration config)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
            _config = config;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ProviderDisplayName { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string ErrorMessage { get; set; }

        public class TokenModel
        {
            public string access_token { get; set; }
            public string token_type { get; set; }
        }

        public class MsMetaData
        {
            [JsonProperty("@odata.mediaContentType")]
            public string odatamediaContentType { get; set; }
            public string id { get; set; }
            public int width { get; set; }
            public int height { get; set; }
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            

            [Required]
            [StringLength(200, MinimumLength = 2)]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Required]
            [StringLength(200, MinimumLength = 2)]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            public string PictureUrl { get; set; }

            public byte[] PictureData { get; set; }

            [StringLength(200)]
            public string PictureContentType { get; set; }

            public string BytesAsString
            {
                get
                {
                    if (PictureData != null)
                    {
                        return Convert.ToBase64String(PictureData);
                    }
                    else return "";

                }
            }
        }
        
        public IActionResult OnGet() => RedirectToPage("./Login");

        public IActionResult OnPost(string provider, string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Page("./ExternalLogin", pageHandler: "Callback", values: new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        public async Task<IActionResult> OnGetCallbackAsync(string returnUrl = null, string remoteError = null)
        {
            IConfigurationSection FBAuthNSection = this._config.GetSection("Authentication:FB");
            string facebookAppId = FBAuthNSection["ClientId"];
            string facebookAppSecret = FBAuthNSection["ClientSecret"];
            returnUrl = returnUrl ?? Url.Content("~/");
            if (remoteError != null)
            {
                ErrorMessage = $"Error from external provider: {remoteError}";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ErrorMessage = "Error loading external login information.";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (result.Succeeded)
            {
                _logger.LogInformation("{Name} logged in with {LoginProvider} provider.", info.Principal.Identity.Name, info.LoginProvider);
                return LocalRedirect(returnUrl);
            }
            if (result.IsLockedOut)
            {
                return RedirectToPage("./Lockout");
            }
            else
            {
                // If the user does not have an account, then ask the user to create an account.
                ReturnUrl = returnUrl;
                ProviderDisplayName = info.ProviderDisplayName;
                if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Email))
                {
                    var id = info.Principal.FindFirstValue(ClaimTypes.NameIdentifier);
                    Input = new InputModel
                    {
                        FirstName = info.Principal.FindFirstValue(ClaimTypes.Surname),
                        LastName = info.Principal.FindFirstValue(ClaimTypes.GivenName),
                        Email = info.Principal.FindFirstValue(ClaimTypes.Email)
                    };
                    if (info.ProviderDisplayName == "Facebook")
                    {
                        var access_token_json = new WebClient().DownloadString($"https://graph.facebook.com/oauth/access_token?client_id={facebookAppId}&client_secret={facebookAppSecret}&grant_type=client_credentials");
                        var token = JsonConvert.DeserializeObject<TokenModel>(access_token_json);
                        Input.PictureUrl = $"https://graph.facebook.com/{id}/picture?type=large&access_token={token.access_token}";
                    }
                    else if (info.ProviderDisplayName == "Microsoft")
                    {
                        try {
                            var wc = new WebClient();
                            wc.Headers.Add("Authorization", "Bearer " + info.AuthenticationTokens.FirstOrDefault().Value);
                            Input.PictureData = wc.DownloadData($"https://graph.microsoft.com/beta/users/{id}/photo/$value");
                            var metadata = wc.DownloadString($"https://graph.microsoft.com/beta/users/{id}/photo/");
                            var mdjson = JsonConvert.DeserializeObject<MsMetaData>(metadata);
                            Input.PictureContentType = mdjson.odatamediaContentType;
                        }
                        catch(System.Net.WebException ex) 
                        {
                            // error while downloading user's image
                        }
                    }

                    else if (info.ProviderDisplayName == "Google")
                    {
                        Input.PictureUrl =  info.Principal.FindFirstValue("picture");
                    }

                }
                return Page();
            }
        }

        public async Task<IActionResult> OnPostConfirmationAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            // Get the information about the user from the external login provider
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ErrorMessage = "Error loading external login information during confirmation.";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            if (ModelState.IsValid)
            {
                var user = CreateUser();
                user.FirstName = Input.FirstName;
                user.LastName = Input.LastName;

                if (info.ProviderDisplayName == "Facebook")
                {
                    var wc = new WebClient();
                    user.PhotoData = wc.DownloadData(Input.PictureUrl);
                    user.PhotoContentType = wc.ResponseHeaders["Content-Type"];
                    user.EmailConfirmed = true;
                }
                else if (info.ProviderDisplayName == "Microsoft")
                {
                    user.PhotoData = Input.PictureData;
                    user.PhotoContentType = Input.PictureContentType;
                    user.EmailConfirmed = true;
                }
                else if (info.ProviderDisplayName == "Google")
                {
                    user.PhotoData = Input.PictureData;
                    user.PhotoContentType = Input.PictureContentType;
                    user.EmailConfirmed = true;
                }

                var defaultrole = await _roleManager.FindByNameAsync("Normal User");

                if (defaultrole != null)
                {
                    await _userManager.AddToRoleAsync(user, defaultrole.Name);
                }
                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);

                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await _userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);
                        if (!user.EmailConfirmed)
                        {
                            var userId = await _userManager.GetUserIdAsync(user);
                            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                            var callbackUrl = Url.Page(
                                "/Account/ConfirmEmail",
                                pageHandler: null,
                                values: new { area = "Identity", userId = userId, code = code },
                                protocol: Request.Scheme);

                            await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                            // If account confirmation is required, we need to show the link if we don't have a real email sender
                            if (_userManager.Options.SignIn.RequireConfirmedAccount)
                            {
                                return RedirectToPage("./RegisterConfirmation", new { Email = Input.Email });
                            }
                        }
                        await _signInManager.SignInAsync(user, isPersistent: false, info.LoginProvider);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            ProviderDisplayName = info.ProviderDisplayName;
            ReturnUrl = returnUrl;
            return Page();
        }

        private AppUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<AppUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(AppUser)}'. " +
                    $"Ensure that '{nameof(AppUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the external login page in /Areas/Identity/Pages/Account/ExternalLogin.cshtml");
            }
        }

        private IUserEmailStore<AppUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<AppUser>)_userStore;
        }

       
    }
}
