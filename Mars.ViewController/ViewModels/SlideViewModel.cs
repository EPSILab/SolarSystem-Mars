using SolarSystem.Mars.Model.ManagersService;
using SolarSystem.Mars.ViewController.Resources;
using System.ComponentModel.DataAnnotations;

namespace SolarSystem.Mars.ViewController.ViewModels
{
    /// <summary>
    /// View-model for slides creation or updating page
    /// </summary>
    public class SlideViewModel
    {
        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public SlideViewModel()
        {
            IsPublished = true;
        }

        /// <summary>
        /// Build a view-model from an entity
        /// </summary>
        /// <param name="slide">Entity to transform</param>
        public SlideViewModel(Slide slide)
        {
            Id = slide.Id;
            ImageRemoteUrl = slide.ImageUrl;
            IsPublished = slide.IsPublished;
            Name = slide.Name;
            Presentation = slide.Presentation;
            Url = slide.Url;
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
        /// Image URL
        /// </summary>
        [Display(ResourceType = typeof(ContentRessources), Name = "Image")]
        [DataType(DataType.ImageUrl, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "ImagePathFormat")]
        [FileExtensions(Extensions = "png, jpg, jpeg, gif", ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "ImagePathExtensions", ErrorMessage = null)]
        public string ImageRemoteUrl { get; set; }

        /// <summary>
        /// Short text
        /// </summary>
        [Display(ResourceType = typeof(ContentRessources), Name = "Presentation")]
        public string Presentation { get; set; }

        /// <summary>
        /// Is the slide published
        /// </summary>
        [Display(ResourceType = typeof(ContentRessources), Name = "IsPublished", ShortName = "IsPublished")]
        public bool IsPublished { get; set; }

        #endregion
    }
}