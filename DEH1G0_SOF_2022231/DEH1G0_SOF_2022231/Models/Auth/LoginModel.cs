using System.ComponentModel.DataAnnotations;
using DEH1G0_SOF_2022231.Models.Helpers.ModelBinders;
using Microsoft.AspNetCore.Mvc;

namespace DEH1G0_SOF_2022231.Models.Auth;

    /// <summary>
    /// Represents a model for user login credentials.
    /// </summary>
    [ModelBinder(typeof(LoginModelBinder))]
    public class LoginModel
    {
        /// <summary>
        /// Gets or sets the user's username.
        /// </summary>
        [StringLength(200, MinimumLength = 3)]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the user's password.
        /// </summary>
        [StringLength(200, MinimumLength = 8)]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }


