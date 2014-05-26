using System;
using System.ComponentModel.DataAnnotations;
using SolarSystem.Mars.Model.ManagersService;
using SolarSystem.Mars.ViewController.Infrastructure.Abstract;
using SolarSystem.Mars.ViewController.Resources;

namespace SolarSystem.Mars.ViewController.ViewModels.Concrete
{
    /// <summary>
    /// View-model for shows creation or updating page
    /// </summary>
    public class ShowViewModel
    {
        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ShowViewModel()
        {
            StartDate = StartTime = DateTime.Now;
            EndDate = EndTime = DateTime.Now.AddHours(4);
            IsPublished = true;
            CanUpdate = true;
            CanDelete = true;
        }

        /// <summary>
        /// Build a view-model from an entity
        /// </summary>
        /// <param name="show">Entity to transform</param>
        /// <param name="authProvider"></param>
        public ShowViewModel(Show show, IAuthProvider authProvider)
        {
            Id = show.Id;
            Description = show.Description;

            EndDate = show.End_DateTime.Date;
            EndTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, show.End_DateTime.Hour, show.End_DateTime.Minute, show.End_DateTime.Second);

            ImageRemoteUrl = show.ImageUrl;
            IsPublished = show.IsPublished;
            Name = show.Name;
            Place = show.Place;

            StartDate = show.Start_DateTime.Date;
            StartDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, show.Start_DateTime.Hour, show.Start_DateTime.Minute, show.Start_DateTime.Second);

            Url = show.Url;

            CanUpdate = true;
            CanDelete = (authProvider.LoginViewModel.Role == Role.Bureau);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        [Display(ResourceType = typeof(ContentRessources), Name = "Name")]
        [Required(ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "NameRequired")]
        [MinLength(5, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "NameMinLength")]
        [MaxLength(100, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "NameMaxLength")]
        public string Name { get; set; }

        /// <summary>
        /// Url
        /// </summary>
        [Display(ResourceType = typeof(ContentRessources), Name = "URL")]
        public string Url { get; set; }

        /// <summary>
        /// Start date
        /// </summary>
        [Display(ResourceType = typeof(ContentRessources), Name = "StartDateTime")]
        [Required(ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "StartDateRequired")]
        [DataType(DataType.Date, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "StartDateRequired")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Start Time
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "StartTimeRequired")]
        [DataType(DataType.Time, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "StartTimeRequired")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:t}")]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// End date
        /// </summary>
        [Display(ResourceType = typeof(ContentRessources), Name = "EndDateTime")]
        [Required(ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "EndDateRequired")]
        [DataType(DataType.Date, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "EndDateRequired")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// End Time
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "EndTimeRequired")]
        [DataType(DataType.Time, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "EndTimeRequired")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:t}")]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Place
        /// </summary>
        [Display(ResourceType = typeof(ContentRessources), Name = "Place")]
        [Required(ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "PlaceRequired")]
        public string Place { get; set; }

        /// <summary>
        /// Image URL
        /// </summary>
        [Display(ResourceType = typeof(ContentRessources), Name = "Image")]
        [DataType(DataType.ImageUrl, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "ImagePathFormat")]
        [FileExtensions(Extensions = "png, jpg, jpeg, gif", ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "ImagePathExtensions", ErrorMessage = null)]
        public string ImageRemoteUrl { get; set; }

        /// <summary>
        /// Short text
        /// </summary>
        [Display(ResourceType = typeof(ContentRessources), Name = "Text")]
        [Required(ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "LongTextRequired")]
        [MinLength(5, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "LongTextMinLength")]
        public string Description { get; set; }

        /// <summary>
        /// Is the show published
        /// </summary>
        [Display(ResourceType = typeof(ContentRessources), Name = "IsPublished", ShortName = "IsPublished")]
        public bool IsPublished { get; set; }

        /// <summary>
        /// Specify if the user can update the show or not
        /// </summary>
        public bool CanUpdate { get; set; }

        /// <summary>
        /// Specify if the user can delete the show or not
        /// </summary>
        public bool CanDelete { get; set; }

        #endregion
    }
}