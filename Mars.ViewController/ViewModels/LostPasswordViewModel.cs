
using System.ComponentModel.DataAnnotations;
using SolarSystem.Mars.ViewController.Resources;

namespace SolarSystem.Mars.ViewController.ViewModels
{
    /// <summary>
    /// View-model for password recover
    /// </summary>
    public class LostPasswordViewModel
    {
        /// <summary>
        /// Username
        /// </summary>
        [Display(ResourceType = typeof(ContentRessources), Name = "Username")]
        [Required(ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "UsernameRequired")]
        public string Username { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [DataType(DataType.EmailAddress, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "EmailFormat")]
        public string Email { get; set; }
    }
}