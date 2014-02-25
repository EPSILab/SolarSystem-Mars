using SolarSystem.Mars.ViewController.Helpers;
using SolarSystem.Mars.ViewController.Resources;
using System.ComponentModel.DataAnnotations;

namespace SolarSystem.Mars.ViewController.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "UsernameRequired")]
        [MinLength(4, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "UsernameMinLength")]
        [MaxLength(20, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "UsernameMaxLength")]
        public string Username { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "PasswordRequired")]
        [MinLength(6, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "PasswordMinLength")]
        [DataType(DataType.Password)]
        public string PasswordNonCrypted { get; set; }

        public string PasswordCrypted
        {
            get
            {
                return PasswordEncoderHelper.Encode(PasswordNonCrypted);
            }
        }
    }
}