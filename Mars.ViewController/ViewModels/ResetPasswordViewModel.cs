using System.ComponentModel.DataAnnotations;
using SolarSystem.Mars.ViewController.Resources;

namespace SolarSystem.Mars.ViewController.ViewModels
{
    /// <summary>
    /// View-model for password reset
    /// </summary>
    public class ResetPasswordViewModel
    {
        /// <summary>
        /// Username
        /// </summary>
        [Display(ResourceType = typeof(ContentRessources), Name = "Username")]
        [Required(ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "UsernameRequired")]
        public string Username { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "PasswordRequired")]
        [MinLength(6, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "PasswordMinLength")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Password confirmation
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "ConfirmPasswordRequired")]
        [Compare("Password", ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "SamePasswords")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Key
        /// </summary>
        public string Key { get; set; }
    }
}