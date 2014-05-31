using System.ComponentModel.DataAnnotations;
using SolarSystem.Mars.ViewController.Resources;
using SolarSystem.Mars.ViewController.ViewModels.Abstract;

namespace SolarSystem.Mars.ViewController.ViewModels.Concrete
{
    /// <summary>
    /// View-model for password recover
    /// </summary>
    public class LostPasswordViewModel : ILostPasswordViewModel
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