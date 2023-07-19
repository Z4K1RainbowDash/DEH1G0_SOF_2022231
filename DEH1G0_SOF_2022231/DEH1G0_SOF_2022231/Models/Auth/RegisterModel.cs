using DEH1G0_SOF_2022231.Models.Helpers.ModelBinders;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DEH1G0_SOF_2022231.Models.Auth
{

    /// <summary>
    /// Represents a model for user registration.
    /// </summary>
    [ModelBinder(BinderType = typeof(RegisterModelBinder))]
    public class RegisterModel
    {
        /// <summary>
        /// Gets or sets the user's first name.
        /// </summary>
        [StringLength(200, MinimumLength = 2)]
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the user's last name.
        /// </summary>
        [StringLength(200, MinimumLength = 2)]
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the user's email address.
        /// </summary>
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the user's username.
        /// </summary>
        [StringLength(200, MinimumLength = 3)]
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the user's password.
        /// </summary>
        [StringLength(200, MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the user's profile image.
        /// </summary>
        public IFormFile? Image { get; set; }
    }

}
