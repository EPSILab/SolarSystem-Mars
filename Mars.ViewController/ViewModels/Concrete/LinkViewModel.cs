using System.ComponentModel.DataAnnotations;
using SolarSystem.Mars.Model.ManagersService;
using SolarSystem.Mars.ViewController.Resources;

namespace SolarSystem.Mars.ViewController.ViewModels.Concrete
{
    /// <summary>
    /// View-model for links creation or updating page
    /// </summary>
    public class LinkViewModel
    {
        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public LinkViewModel()
        {
        }

        /// <summary>
        /// Build a view-model from an entity
        /// </summary>
        /// <param name="link">Entity to transform</param>
        public LinkViewModel(Link link)
        {
            Id = link.Id;
            Description = link.Description;
            ImageRemoteUrl = link.ImageUrl;
            Label = link.Label;
            Order = link.Order;
            Url = link.Url;
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
        [Display(ResourceType = typeof(ContentRessources), Name = "Label")]
        [Required(ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "LabelRequired")]
        [MinLength(2, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "LabelMinLength")]
        [MaxLength(100, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "LabelMaxLength")]
        public string Label { get; set; }

        /// <summary>
        /// Url
        /// </summary>
        [Display(ResourceType = typeof(ContentRessources), Name = "URL")]
        public string Url { get; set; }

        /// <summary>
        /// Order
        /// </summary>
        [Display(ResourceType = typeof(ContentRessources), Name = "Order")]
        public int? Order { get; set; }

        /// <summary>
        /// Image URL
        /// </summary>
        [Display(ResourceType = typeof(ContentRessources), Name = "Image")]
        [DataType(DataType.ImageUrl, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "ImagePathFormat")]
        [FileExtensions(Extensions = "png, jpg, jpeg, gif", ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "ImagePathExtensions", ErrorMessage = null)]
        public string ImageRemoteUrl { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [Display(ResourceType = typeof(ContentRessources), Name = "Text")]
        [Required(ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "LongTextRequired")]
        [MinLength(5, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "LongTextMinLength")]
        public string Description { get; set; }

        #endregion
    }
}