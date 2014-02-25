using SolarSystem.Mars.ViewController.Resources;
using System.ComponentModel.DataAnnotations;

namespace SolarSystem.Mars.ViewController.ViewModels
{
    public class RegisterViewModel
    {
        #region Properties

        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "UsernameRequired")]
        [MinLength(4, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "UsernameMinLength")]
        [MaxLength(20, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "UsernameMaxLength")]
        public string Username { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "PasswordRequired")]
        [MinLength(6, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "PasswordMinLength")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "ConfirmPasswordRequired")]
        [Compare("Password", ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "SamePasswords")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "SurnameRequired")]
        public string Surname { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "FirstNameRequired")]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "CityRequired")]
        public string City { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "EmailEPSIRequired")]
        [DataType(DataType.EmailAddress, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "EmailEPSIFormat")]
        public string EmailEPSI { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "EmailPersoFormat")]
        public string EmailPerso { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "PhoneNumberFormat")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "VilleRequired")]
        public int Code_Ville { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "ClasseRequired")]
        public int Code_Classe { get; set; }

        [DataType(DataType.Url, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "WebsiteUrlFormat")]
        public string WebsiteUrl { get; set; }

        [DataType(DataType.Url, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "FacebookUrlFormatError")]
        [RegularExpression(@"(?:\S+\s)?\S*facebook.com\S*(?:\s\S+)?", ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "FacebookUrlFormatError")]
        public string FacebookUrl { get; set; }

        [DataType(DataType.Url, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "TwitterUrlFormatError")]
        [RegularExpression(@"(?:\S+\s)?\S*twitter.com\S*(?:\s\S+)?", ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "TwitterUrlFormatError")]
        public string TwitterUrl { get; set; }

        [DataType(DataType.Url, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "LinkedInUrlFormatError")]
        [RegularExpression(@"(?:\S+\s)?\S*linkedin.com\S*(?:\s\S+)?", ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "LinkedInUrlFormatError")]
        public string LinkedInUrl { get; set; }

        [DataType(DataType.Url, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "ViadeoUrlFormatError")]
        [RegularExpression(@"(?:\S+\s)?\S*viadeo.com\S*(?:\s\S+)?", ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "ViadeoUrlFormatError")]
        public string ViadeoUrl { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "PresentationRequired")]
        [DataType(DataType.Text, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "PresentationRequired")]
        [MinLength(30, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "PresentationRequired")]
        public string Presentation { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "MotivationsRequired")]
        [DataType(DataType.Text, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "MotivationsRequired")]
        [MinLength(30, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "MotivationsRequired")]
        public string Motivations { get; set; }

        #endregion
    }
}