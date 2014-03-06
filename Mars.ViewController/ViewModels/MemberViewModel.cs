﻿using Mars.Common;
using SolarSystem.Mars.ViewController.Resources;
using System.ComponentModel.DataAnnotations;

namespace SolarSystem.Mars.ViewController.ViewModels
{
    /// <summary>
    /// View-model for member creation, updating or validation page
    /// </summary>
    public class MemberViewModel
    {
        #region Properties

        /// <summary>
        /// Username
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "UsernameRequired")]
        [MinLength(4, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "UsernameMinLength")]
        [MaxLength(20, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "UsernameMaxLength")]
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
        /// Last name
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "SurnameRequired")]
        public string LastName { get; set; }

        /// <summary>
        /// First name
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "FirstNameRequired")]
        public string FirstName { get; set; }

        /// <summary>
        /// City from
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "CityFromRequired")]
        public string CityFrom { get; set; }

        /// <summary>
        /// EPSI email
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "EPSIEmailRequired")]
        [DataType(DataType.EmailAddress, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "EPSIEmailFormat")]
        public string EPSIEmail { get; set; }

        /// <summary>
        /// Personal email
        /// </summary>
        [DataType(DataType.EmailAddress, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "PersonalEmailFormat")]
        public string PersonalEmail { get; set; }

        /// <summary>
        /// Phone number
        /// </summary>
        [DataType(DataType.EmailAddress, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "PhoneNumberFormat")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Campus's Id
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "CampusRequired")]
        public int IdCampus { get; set; }

        /// <summary>
        /// Promotion's Id
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "PromoRequired")]
        public int IdPromotion { get; set; }

        /// <summary>
        /// Website
        /// </summary>
        [DataType(DataType.Url, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "WebsiteUrlFormat")]
        public string Website { get; set; }

        /// <summary>
        /// Facebook page
        /// </summary>
        [DataType(DataType.Url, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "FacebookUrlFormatError")]
        [RegularExpression(@"(?:\S+\s)?\S*facebook.com\S*(?:\s\S+)?", ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "FacebookUrlFormatError")]
        public string FacebookUrl { get; set; }

        /// <summary>
        /// Twitter page
        /// </summary>
        [DataType(DataType.Url, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "TwitterUrlFormatError")]
        [RegularExpression(@"(?:\S+\s)?\S*twitter.com\S*(?:\s\S+)?", ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "TwitterUrlFormatError")]
        public string TwitterUrl { get; set; }

        /// <summary>
        /// LinkedIn page
        /// </summary>
        [DataType(DataType.Url, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "LinkedInUrlFormatError")]
        [RegularExpression(@"(?:\S+\s)?\S*linkedin.com\S*(?:\s\S+)?", ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "LinkedInUrlFormatError")]
        public string LinkedInUrl { get; set; }

        /// <summary>
        /// Viadeo page
        /// </summary>
        [DataType(DataType.Url, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "ViadeoUrlFormatError")]
        [RegularExpression(@"(?:\S+\s)?\S*viadeo.com\S*(?:\s\S+)?", ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "ViadeoUrlFormatError")]
        public string ViadeoUrl { get; set; }

        /// <summary>
        /// GitHub page
        /// </summary>
        [DataType(DataType.Url, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "GitHubUrlFormatError")]
        [RegularExpression(@"(?:\S+\s)?\S*github.com\S*(?:\s\S+)?", ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "GitHubUrlFormatError")]
        public string GitHubUrl { get; set; }

        /// <summary>
        /// Presentation
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "PresentationRequired")]
        [DataType(DataType.Text, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "PresentationRequired")]
        [MinLength(30, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "PresentationRequired")]
        public string Presentation { get; set; }

        #endregion
    }
}