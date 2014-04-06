using SolarSystem.Mars.Model.ManagersService;
using SolarSystem.Mars.ViewController.Resources;
using System.ComponentModel.DataAnnotations;

namespace SolarSystem.Mars.ViewController.ViewModels
{
    /// <summary>
    /// View-model for campuses creation or updating page
    /// </summary>
    public class CampusViewModel
    {
        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CampusViewModel()
        {
        }

        /// <summary>
        /// Build a view-model from an entity
        /// </summary>
        /// <param name="campus">Entity to transform</param>
        public CampusViewModel(Campus campus)
        {
            Id = campus.Id;
            ContactEmail = campus.ContactEmail;
            Place = campus.Place;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Place
        /// </summary>
        [Display(ResourceType = typeof(ContentRessources), Name = "Place")]
        [Required(ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "PlaceRequired")]
        public string Place { get; set; }

        /// <summary>
        /// EPSI email
        /// </summary>
        [Display(ResourceType = typeof(ContentRessources), Name = "ContactEmail")]
        [Required(ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "ContactEmailRequired")]
        [DataType(DataType.EmailAddress, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "EmailFormat")]
        public string ContactEmail { get; set; }

        #endregion
    }
}