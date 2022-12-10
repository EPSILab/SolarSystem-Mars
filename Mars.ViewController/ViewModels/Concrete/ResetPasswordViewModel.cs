using System.ComponentModel.DataAnnotations;
using SolarSystem.Mars.ViewController.Resources;
using SolarSystem.Mars.ViewController.ViewModels.Abstract;

namespace SolarSystem.Mars.ViewController.ViewModels.Concrete
{
    /// <summary>
    /// View-model for password reset
    /// </summary>
    public class ResetPasswordViewModel : PasswordViewModel, IResetPasswordViewModel
    {
        /// <summary>
        /// Username
        /// </summary>
        [Display(ResourceType = typeof(ContentRessources), Name = "Username")]
        [Required(ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "UsernameRequired")]
        public string Username { get; set; }

        /// <summary>
        /// Key
        /// </summary>
        public string Key { get; set; }
    }
}