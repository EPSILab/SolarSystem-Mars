using SolarSystem.Mars.Model.ManagersService;
using SolarSystem.Mars.ViewController.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace SolarSystem.Mars.ViewController.ViewModels
{
    /// <summary>
    /// View-model for conferences creation or updating page
    /// </summary>
    public class ConferenceViewModel
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ConferenceViewModel()
        {
            StartDate = StartTime = DateTime.Now;
            EndDate = EndTime = DateTime.Now.AddHours(4);
            IsPublished = true;
        }

        /// <summary>
        /// Build a view-model from an entity
        /// </summary>
        /// <param name="conference">Entity to transform</param>
        public ConferenceViewModel(Conference conference)
        {
            Id = conference.Id;
            CampusId = conference.Campus.Id;
            CampusName = conference.Campus.Place;
            Description = conference.Description;

            EndDate = conference.End_DateTime.Date;
            EndTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, conference.End_DateTime.Hour, conference.End_DateTime.Minute, conference.End_DateTime.Second);
            
            ImageRemoteUrl = conference.ImageUrl;
            IsPublished = conference.IsPublished;
            Name = conference.Name;
            Place = conference.Place;

            StartDate = conference.Start_DateTime.Date;
            StartDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, conference.Start_DateTime.Hour, conference.Start_DateTime.Minute, conference.Start_DateTime.Second);

            Url = conference.Url;
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
        /// Campus Id
        /// </summary>
        [Display(ResourceType = typeof(ContentRessources), Name = "Campus")]
        [Required(ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "CampusRequired")]
        [Range(1, Int32.MaxValue, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "CampusRequired")]
        public int CampusId { get; set; }

        /// <summary>
        /// Campus name
        /// </summary>
        [Display(ResourceType = typeof(ContentRessources), Name = "Campus")]
        public string CampusName { get; set; }

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
        /// Is the conference published
        /// </summary>
        [Display(ResourceType = typeof(ContentRessources), Name = "IsPublished", ShortName = "IsPublished")]
        public bool IsPublished { get; set; }

        #endregion
    }
}