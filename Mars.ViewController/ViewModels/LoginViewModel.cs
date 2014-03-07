﻿using System.ComponentModel;
using SolarSystem.Mars.ViewController.Helpers;
using SolarSystem.Mars.ViewController.Resources;
using System.ComponentModel.DataAnnotations;

namespace SolarSystem.Mars.ViewController.ViewModels
{
    /// <summary>
    /// View-model for login page
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// Username
        /// </summary>
        [Display(ResourceType = typeof(ContentRessources), Name = "Username")]
        [Required(ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "UsernameRequired")]
        [MinLength(4, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "UsernameMinLength")]
        [MaxLength(20, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "UsernameMaxLength")]
        public string Username { get; set; }

        /// <summary>
        /// Not-crypted password
        /// </summary>
        [Display(ResourceType = typeof(ContentRessources), Name = "Password")]
        [Required(ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "PasswordRequired")]
        [MinLength(6, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "PasswordMinLength")]
        [DataType(DataType.Password)]
        public string PasswordNonCrypted { get; set; }

        /// <summary>
        /// Crypted password
        /// </summary>
        public string PasswordCrypted
        {
            get { return PasswordEncoder.Encode(PasswordNonCrypted); }
        }
    }
}