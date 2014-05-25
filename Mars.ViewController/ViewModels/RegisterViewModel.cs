using System.ComponentModel.DataAnnotations;
using SolarSystem.Mars.ViewController.Resources;
using SolarSystem.Mars.ViewController.ViewModels.Abstract;

namespace SolarSystem.Mars.ViewController.ViewModels
{
    public class RegisterViewModel : MemberViewModel, IPasswordViewModel
    {
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
    }
}